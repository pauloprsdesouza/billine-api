using Billine.Admin.Infrastructure.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api.Dependencies
{
    public static class MapperProfileDependency
    {
        public static void AddMapperProfiles(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(typeof(CompanyProfile),
                                   typeof(OrderItemProfile),
                                   typeof(OrderProfile),
                                   typeof(ProductProfile),
                                   typeof(UserProfile));
        }
    }
}
