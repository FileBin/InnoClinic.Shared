namespace Shared.Misc;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Misc.Middleware;

public static class ConfigureServices {
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
    }
}
