using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;
using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Repository;
using InnoClinic.Shared.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Shared.LayeredWebApp.ApplicationLayer;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
        services.AddDbContext<TestDbContext>(options =>
            options.UseSqlite("Data Source=testdb"));
        services.AddRepositoriesFromAssembly(typeof(TestDbContext).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
