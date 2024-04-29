using System.Security.Claims;
using InnoClinic.Shared.Misc;
using Microsoft.AspNetCore.Authorization;

namespace InnoClinic.Shared.TestWebApp;

public class Program {
    static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddUtils()
            .AddIdentityServer(builder.Configuration, builder.Environment);

        var app = builder.Build();

        app.UseUtils();

        app.MapGet("/userinfo", [Authorize] (ClaimsPrincipal user) => user.Claims.Select(x => new { x.Type, x.Value }));

        app.UseIdentityServer();
        app.Run();
    }
}