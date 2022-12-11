using ApiIntegratedWithDocker.Domain.Requests;
using ApiIntegratedWithDocker.Infrastructure.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using OrderModel = ApiIntegratedWithDocker.Domain.Models.Order;

namespace ApiIntegratedWithDocker.Domain.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(OrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderModel> Order(OrderRequest orderRequest)
    {
        var order = _mapper.Map<OrderModel>(orderRequest);

        order.Id = await _orderRepository.Insert(order);

        return order;
    }
}
