using System;

namespace ApiIntegratedWithDocker.Domain.Models;

public class Order
{
    public Order() => Id = Guid.NewGuid();

    public Guid Id { get; set; }
}
