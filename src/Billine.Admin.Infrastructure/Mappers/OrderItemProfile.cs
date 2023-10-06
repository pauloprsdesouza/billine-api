using AutoMapper;
using Billine.Admin.Contracts.OrderItems;
using Billine.Admin.Domain.OrderItems;
using Billine.Admin.Domain.Sefaz.Models;
using Billine.Admin.Infrastructure.Database.DataModel.OrderItems;

namespace Billine.Admin.Infrastructure.Mappers
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemModel, OrderItem>().ReverseMap();
            CreateMap<OrderItem, OrderItemResponse>();
            CreateMap<NFCeItem, OrderItem>();
        }
    }
}
