using Shared.Misc;

namespace InnoClinic.Shared.TestWebApp;

class Program {
    static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddUtils()
            .AddIdentityServer(builder.Configuration, builder.Environment);

        IdentityModelEventSource.ShowPII = true;

        var app = builder.Build();

        app.UseUtils();

        if (!app.Environment.IsProduction()) {
            app.MapGet("/userinfo", [Authorize] (ClaimsPrincipal user) => user.Claims.Select(x => new { x.ValueType, x.Value }));
        }
        app.MapControllers();

        app.UseIdentityServer();
        app.Run();

    }
}