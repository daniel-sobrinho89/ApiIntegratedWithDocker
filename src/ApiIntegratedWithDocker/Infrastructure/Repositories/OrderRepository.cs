using ApiIntegratedWithDocker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIntegratedWithDocker.Infrastructure.Repositories;

public class OrderRepository
{
    private readonly DbContextEf _dbContext;

    public OrderRepository(DbContextEf dbContext)
        => _dbContext = dbContext;

    public virtual async Task<List<Order>> Get() =>
        await _dbContext.Order
            .ToListAsync();
}