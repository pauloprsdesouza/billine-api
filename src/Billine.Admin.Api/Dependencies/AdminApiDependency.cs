using System;
using Billine.Admin.Api.Configurations;
using Billine.Admin.Domain.Sefaz;
using Billine.Admin.Infrastructure.Apis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api.Dependencies
{
    public static class AdminApiDependency
    {
        public static void AddAdminApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiCredentialOption>(configuration);

            services.AddHttpClient<ISefazProvider, SefazProvider>("Admin", httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration[nameof(ApiCredentialOption.BaseUrl)]);
            });
        }
    }
}
