using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace Shared.Misc;

public class AuthorizeCheckOperationFilter(IConfiguration config) : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var scope = config.GetOrThrow("IdentityServer:ApiScope:Name");

        var hasAuthorize =
          (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ?? false)
          || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (hasAuthorize) {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement> {
                new() {
                    [new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    }] = [scope]
                }
            };

        }
    }
}