namespace InnoClinic.Shared.Misc;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.Extensions.Logging;
using InnoClinic.Shared.Misc.Middleware;
using System.Reflection;
using InnoClinic.Shared.Misc.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Routing;
using InnoClinic.Shared.Domain.Abstractions;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

public static class ConfigureServices {
    public static void AddLogger(this WebApplicationBuilder builder) {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.AddSerilog(logger);
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly) {
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null) {
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints) {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }

    public static IServiceCollection AddRepositoriesFromAssembly(this IServiceCollection services, Assembly assembly) {
        assembly.GetTypes()
            .Select(type => (service:
                type.GetInterfaces().Where(t => t.IsGenericType).FirstOrDefault(t =>
                    t.GetGenericTypeDefinition() == typeof(IRepository<>)),
                implementation: type))
            .Where(tuple => tuple.service is not null)
            .ToList()
            .ForEach(pair => services.AddScoped(pair.service!, pair.implementation));

        return services;
    }


    public static IServiceCollection AddUtils(this IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails(options =>
            options.CustomizeProblemDetails = ctx => {
                var ext = ctx.ProblemDetails.Extensions;

                if (!ext.ContainsKey("traceId")) {
                    ext.Add("traceId", ctx.HttpContext.TraceIdentifier);
                }
                if (!ext.ContainsKey("instance")) {
                    ext.Add("instance", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                }
            });
        return services;
    }

    public static void UseUtils(this WebApplication app) {
        app.UseUtils(app.Environment);
    }

    public static void UseUtils(this IApplicationBuilder app, IHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        app.UseStatusCodePages();
    }

    public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration config, IHostEnvironment env) {
        var isconf = config.GetSection("IdentityServer");

        var title = config["SwaggerTitle"] ?? "Protected API";

        var apiName = isconf.GetOrThrow("ApiName");
        var authority = isconf.GetOrThrow("Authority");
        var apiScope = isconf.GetSection("ApiScope");

        var clientHandler = new HttpClientHandler {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        };

        services.AddAuthentication(options => {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.Authority = authority;
                options.RequireHttpsMetadata = false;
                options.Audience = apiName;
                if (!env.IsProduction()) {
                    options.BackchannelHttpHandler = clientHandler;
                }
            });

        services.AddAuthorization(options => {
            options.AddPolicy("IdentityScope", policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", apiScope.GetOrThrow("Name"));
            });
        });

        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows {
                    AuthorizationCode = new OpenApiOAuthFlow {
                        AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                        TokenUrl = new Uri($"{authority}/connect/token"),
                        Scopes = new Dictionary<string, string> {
                            { apiScope.GetOrThrow("Name"), apiScope.GetOrThrow("Desc") }
                        }
                    }
                }
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }

    public static void UseIdentityServer(this WebApplication app) {
        app.UseAuthentication();
        app.UseAuthorization();


        var title = app.Configuration["SwaggerTitle"] ?? "Protected API";

        var isconf = app.Configuration.GetSection("IdentityServer:OAuth");

        var clientId = isconf.GetOrThrow("ClientId");
        var clientSecret = isconf.GetOrThrow("ClientSecret");
        var appName = isconf.GetOrThrow("AppName");

        if (!app.Environment.IsProduction()) {
            app.UseSwagger();

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{title} V1");

                options.OAuthClientId(clientId);
                options.OAuthClientSecret(clientSecret);
                options.OAuthAppName(appName);
                options.OAuthUsePkce();
            });
        }
    }

    public static async Task EnsureDatabaseCreated<T>(this WebApplication app, bool migrate = false) where T : DbContext {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(serviceScope);

        var context = serviceScope.ServiceProvider.GetRequiredService<T>();
        try {
            await context.Database.EnsureCreatedAsync();
            if (migrate) {
                await context.Database.MigrateAsync();
            }
        } catch (SqlException e) {
            app.Logger.LogWarning(e, "Database creating exited with errors");
        }
    }
}
