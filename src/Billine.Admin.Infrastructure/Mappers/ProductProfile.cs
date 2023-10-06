using AutoMapper;
using Billine.Admin.Contracts.Products;
using Billine.Admin.Domain.Products;
using Billine.Admin.Infrastructure.Database.DataModel.Products;

namespace Billine.Admin.Infrastructure.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<Product, ProductResponse>();
        }
    }
}
