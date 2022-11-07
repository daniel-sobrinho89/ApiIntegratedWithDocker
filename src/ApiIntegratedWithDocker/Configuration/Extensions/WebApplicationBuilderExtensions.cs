using ApiIntegratedWithDocker.Infrastructure;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiIntegratedWithDocker.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddRepositories(this IServiceCollection services) =>
        services.AddScoped<OrderRepository>();

    public static void AddDBContext(this IServiceCollection services) => services.AddDbContext<DbContextEf>((services, builder) 
        =>
            {
                builder.UseNpgsql(services.GetService<IConfiguration>().GetConnectionString("DefaultConnection"),
                    options => options.CommandTimeout(300));
            });
}