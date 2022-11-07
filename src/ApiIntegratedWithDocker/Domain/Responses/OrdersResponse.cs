using ApiIntegratedWithDocker.Domain.Models;
using System.Collections.Generic;

namespace ApiIntegratedWithDocker.Domain.Responses;

public class OrdersResponse
{
    public List<Order> Items { get; set; }

    public OrdersResponse(List<Order> orders) 
        => Items = orders;
}