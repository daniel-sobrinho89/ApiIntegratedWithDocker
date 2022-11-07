using ApiIntegratedWithDocker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiIntegratedWithDocker.Infrastructure;

public class DbContextEf : DbContext
{
    public DbContextEf() { }

    public DbContextEf(
        DbContextOptions<DbContextEf> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Order> Order { get; set; }
}
