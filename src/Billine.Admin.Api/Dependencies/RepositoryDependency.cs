using Billine.Admin.Domain.Companies;
using Billine.Admin.Domain.Orders;
using Billine.Admin.Domain.Products;
using Billine.Admin.Domain.Users;
using Billine.Admin.Infrastructure.Database.DataModel.Companies;
using Billine.Admin.Infrastructure.Database.DataModel.Orders;
using Billine.Admin.Infrastructure.Database.DataModel.Products;
using Billine.Admin.Infrastructure.Database.DataModel.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api.Dependencies
{
    public static class RepositoryDependency
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            _ = services.AddScoped<ICompanyRepository, CompanyRepository>();
            _ = services.AddScoped<IUserRepository, UserRepository>();
            _ = services.AddScoped<IOrderRepository, OrderRepository>();
            _ = services.AddScoped<IProductRepository, ProductRepository>();
     
        }
    }
}
