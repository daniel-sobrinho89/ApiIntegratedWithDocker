using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderModel = ApiIntegratedWithDocker.Domain.Models.Order;

namespace ApiIntegratedWithDocker.Infrastructure.Repositories;

public class OrderRepository
{
    private readonly DbContextEf _dbContext;

    public OrderRepository(DbContextEf dbContext)
        => _dbContext = dbContext;

    public virtual async Task<List<OrderModel>?> Get() =>
        await _dbContext.Order
            .ToListAsync();

    public virtual async Task<OrderModel?> GetById(Guid id) =>
        await _dbContext.Order
            .FirstOrDefaultAsync(x => x.Id == id);

    public virtual async Task<Guid> Insert(OrderModel order)
    {
        await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order.Id;
    }
}