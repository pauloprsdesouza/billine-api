using Billine.Admin.Api.Authorization;
using Billine.Admin.Api.Configurations;
using Billine.Admin.Api.Dependencies;
using Billine.Admin.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddDefaultAWSOptions(_configuration.GetAWSOptions());
            _ = services.AddControllers(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                _ = options.Filters.Add(typeof(ExceptionFilter));
                _ = options.Filters.Add(typeof(RequestValidationFilter));
                _ = options.Filters.Add(typeof(NotificationFilter));
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.Default());

            services.AddDefaultCorsPolicy();
            services.AddServices();
            services.AddRepositories();
            services.AddMapperProfiles();
            services.AddNotifications();
            services.AddDynamoDBDependency(_configuration);
            services.AddSwaggerDocumentation();
            services.AddMemoryCache();

            services.AddAdminApi(_configuration.GetSection("AdminApi"));
            services.AddJwtAuthentication(_configuration.GetSection("JWT"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerDocumentation();

            _ = app.UseRouting();

            _ = app.UseCors();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
