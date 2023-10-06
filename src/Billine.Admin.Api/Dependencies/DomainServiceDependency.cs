using Billine.Admin.Application.Companies;
using Billine.Admin.Application.Orders;
using Billine.Admin.Application.Products;
using Billine.Admin.Application.Users;
using Billine.Admin.Domain.Companies;
using Billine.Admin.Domain.Orders;
using Billine.Admin.Domain.Products;
using Billine.Admin.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api.Dependencies
{
    public static class DomainServiceDependency
    {
        public static void AddServices(this IServiceCollection services)
        {
            _ = services.AddScoped<ICompanyService, CompanyService>();
            _ = services.AddScoped<IOrderService, OrderService>();
            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IUserService, UserService>();
        }
    }
}
