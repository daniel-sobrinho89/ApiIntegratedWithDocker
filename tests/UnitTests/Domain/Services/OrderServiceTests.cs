using ApiIntegratedWithDocker.Domain.Services;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using Autofac.Extras.FakeItEasy;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Tests.Common.Builders.Domain.Models;
using Tests.Common.Builders.Domain.Requests;
using OrderModel = ApiIntegratedWithDocker.Domain.Models.Order;

namespace UnitTests.Domain.Services;

public class OrderServiceTests : BaseTests
{
    [SetUp]
    public void SetUp() => 
        AutoFake.Provide(A.Fake<OrderRepository>());

    [Test]
    public async Task ShouldReturnSuccessWhenCreatingANewOrder()
    {
        var expectedOrder = new OrderBuilder().Generate();

        var orderRequest = new OrderRequestBuilder()
            .Generate();

        A.CallTo(() => AutoFake
            .Resolve<IMapper>()
            .Map<OrderModel>(orderRequest))
            .Returns(expectedOrder);

        A.CallTo(() => AutoFake
            .Resolve<OrderRepository>()
            .Insert(expectedOrder))
            .Returns(expectedOrder.Id);

        var serviceResult = await AutoFake.Resolve<OrderService>()
            .Order(orderRequest);

        serviceResult.Should().Be(expectedOrder);
    }
}
