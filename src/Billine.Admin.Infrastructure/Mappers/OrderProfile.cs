using AutoMapper;
using Billine.Admin.Contracts.Orders;
using Billine.Admin.Domain.Orders;
using Billine.Admin.Domain.Sefaz.Models;
using Billine.Admin.Infrastructure.Database.DataModel.Orders;

namespace Billine.Admin.Infrastructure.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderModel, Order>().ReverseMap();
            CreateMap<Order, OrderResponse>();
            CreateMap<NFCe, Order>();

        }
    }
}
