using ApiIntegratedWithDocker.Common;
using ApiIntegratedWithDocker.Domain.Requests;
using ApiIntegratedWithDocker.Domain.Responses;
using ApiIntegratedWithDocker.Domain.Services;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using OrderModel = ApiIntegratedWithDocker.Domain.Models.Order;

namespace ApiIntegratedWithDocker.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderService orderService, OrderRepository orderRepository)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get a list of orders.",
        OperationId = "GetOrders",
        Tags = new[] { "Orders" }
    )]
    [SwaggerResponse(200, "The orders.", typeof(OrdersResponse), ContentTypes = new string[] { "application/json" })]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderRepository.Get();

        if (orders is null || !orders.Any())
        {
            ApiProblem[] errors = { new ApiProblem(ErrorCodes.EmptyQuery, ErrorDescriptions.NullQuery) };
            var problemDetails = new ApiValidationProblemDetails(errors, HttpContext.Request.Path, 404);
            return NotFound(problemDetails);
        }

        var ordersResponse = new OrdersResponse(orders);

        return Ok(ordersResponse);
    }


    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(
        Summary = "Get an order by Id.",
        OperationId = "GetOrder",
        Tags = new[] { "Orders" }
    )]
    [SwaggerResponse(200, "The order.", typeof(OrderModel), ContentTypes = new string[] { "application/json" })]
    public async Task<IActionResult> GetOrder(Guid id)
    {

        var orderResult = await _orderRepository.GetById(id);

        if (orderResult is null)
        {
            ApiProblem[] errors = { new ApiProblem(ErrorCodes.EmptyQuery, ErrorDescriptions.NullQuery) };
            var problemDetails = new ApiValidationProblemDetails(errors, HttpContext.Request.Path, 404);
            return NotFound(problemDetails);
        }

        return Ok(orderResult);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new order.",
        OperationId = "PostOrder",
        Tags = new[] { "Orders" }
    )]
    [SwaggerResponse(200, "The new order.", typeof(OrdersResponse), ContentTypes = new string[] { "application/json" })]
    public async Task<IActionResult> PostOrder(OrderRequest orderRequest)
    {
        var orderResult = await _orderService.Order(orderRequest);

        return CreatedAtAction(nameof(GetOrder),
            new { id = orderResult.Id }, orderResult);
    }
}
