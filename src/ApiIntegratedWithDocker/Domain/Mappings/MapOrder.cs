using ApiIntegratedWithDocker.Domain.Requests;
using AutoMapper;
using OrderModel = ApiIntegratedWithDocker.Domain.Models.Order;

namespace ApiIntegratedWithDocker.Domain.Mappings;

public class MapOrder : Profile
{
    public MapOrder()
        => CreateMap<OrderRequest, OrderModel>();
}