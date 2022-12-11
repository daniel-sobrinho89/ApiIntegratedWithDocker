using ApiIntegratedWithDocker.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Common.Builders.Domain.Models;
using Tests.Common.Infrastructure;

namespace IntegrationTests.DatabaseTests.Repositories;

public class OrderRepositoryTests : IntegrationTestsBase
{
    private OrderRepository _orderRepository;
    
    [SetUp]
    public void SetUp() => _orderRepository = ServiceProvider.GetRequiredService<OrderRepository>();

    [Test]
    public async Task ShouldCreateOrder()
    {
        Clock.FreezeCurrentDate();

        var order = new OrderBuilder()
            .Generate();

        await _orderRepository.Insert(order);

        var returnedOrder = ContextForAsserts.Order
            .FirstOrDefault();

        returnedOrder.Should().BeEquivalentTo(order);
    }
}
