using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;
using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Repository;
using InnoClinic.Shared.Misc;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Shared.LayeredWebApp.ApplicationLayer;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
        services.AddDbContext<TestDbContext>();
        services.AddRepositoriesFromAssembly(typeof(TestDbContext).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication application) {
        using (var serviceScope = application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
            var context = serviceScope.ServiceProvider.GetRequiredService<TestDbContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        return application;
    }
}
