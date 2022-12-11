using ApiIntegratedWithDocker.Domain.Services;
using ApiIntegratedWithDocker.Infrastructure;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiIntegratedWithDocker.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(Program));

    public static void AddServices(this IServiceCollection services) =>
        services.AddScoped<OrderService>();

    public static void AddRepositories(this IServiceCollection services) =>
        services.AddScoped<OrderRepository>();

    public static void AddDBContext(this IServiceCollection services) => services.AddDbContext<DbContextEf>((services, builder) 
        =>
            {
                builder.UseNpgsql(services.GetService<IConfiguration>().GetConnectionString("DefaultConnection"),
                    options => options.CommandTimeout(300));
            });
}