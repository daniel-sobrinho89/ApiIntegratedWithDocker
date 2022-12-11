using ApiIntegratedWithDocker.Common;
using ApiIntegratedWithDocker.Controllers;
using ApiIntegratedWithDocker.Domain.Services;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace UnitTests.Controller;

internal class OrderControllerTests : BaseTests
{
    [SetUp]
    public void SetUp()
    {
        AutoFake.Provide(A.Fake<OrderService>());
        AutoFake.Provide(A.Fake<OrderRepository>());
    }

    public HttpContext CreateMockHttpContext(HttpStatusCode httpStatusCode = HttpStatusCode.OK, 
        string method = "GET", 
        string path = "/api/v1/orders"
    )
    {
        var request = A.Fake<HttpRequest>();
        A.CallTo(() => request.Method).Returns(method);

        var response = A.Fake<HttpResponse>();
        response.StatusCode = (int)httpStatusCode;

        var mockHttpContext = A.Fake<HttpContext>();
        A.CallTo(() => mockHttpContext.Request).Returns(request);
        A.CallTo(() => mockHttpContext.Response).Returns(response);
        A.CallTo(() => mockHttpContext.Request.Path).Returns(path);

        return mockHttpContext;
    }

    [Test]
    [TestCase(StatusCodes.Status404NotFound)]
    public async Task ShouldReturnNotFoundWhenOrdersDoesNotExist(int statusCode)
    {
        var orderController = new OrderController(A.Fake<OrderService>(), A.Fake<OrderRepository>())
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = CreateMockHttpContext()
            }
        };

        var returnedOrders = await orderController
            .GetOrders();

        var apiProblems = new ApiProblemDetails() { Status = statusCode };

        var objectResult = returnedOrders as ObjectResult;
        objectResult!.StatusCode.Should().Be(apiProblems.Status);
    }
}
