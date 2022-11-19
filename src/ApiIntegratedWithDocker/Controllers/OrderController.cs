using ApiIntegratedWithDocker.Common;
using ApiIntegratedWithDocker.Domain.Responses;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace ApiIntegratedWithDocker.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository) 
        => _orderRepository = orderRepository;

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get a list of orders.",
        OperationId = "GetOrders",
        Tags = new[] { "Orders" }
    )]
    [SwaggerResponse(200, "The order.", typeof(OrdersResponse), ContentTypes = new string[] { "application/json" })]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderRepository.Get();

        if (orders is null)
        {
            ApiProblem[] errors = { new ApiProblem(ErrorCodes.EmptyQuery, ErrorDescriptions.NullQuery) };
            var problemDetails = new ApiValidationProblemDetails(errors, HttpContext.Request.Path, 404);
            return NotFound(problemDetails);
        }

        var ordersResponse = new OrdersResponse(orders);

        return Ok(ordersResponse);
    }
}
