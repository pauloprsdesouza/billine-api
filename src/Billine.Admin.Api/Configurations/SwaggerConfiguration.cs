using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Billine.Admin.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        private const string Title = "Open Agenda Admin API";
        private const string Description = "Open Agenda Admin API V1";
        private const string Version = "v1";

        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(options =>
            {
                string xmlComments = Path.Combine(AppContext.BaseDirectory, "Billine.Admin.Api.xml");

                options.SwaggerDoc(Version, new OpenApiInfo
                {
                    Title = Title,
                    Description = Description,
                    Version = Version
                });

                options.AddSecurityDefinition("api-key", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "api-key",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });

                options.IncludeXmlComments(xmlComments);
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            _ = app.UseSwagger(options =>
            {
                options.RouteTemplate = "docs/swagger/{documentname}/swagger.json";
            });

            _ = app.UseReDoc(options =>
            {
                options.DocumentTitle = Title;
                options.RoutePrefix = "docs";
                options.SpecUrl($"swagger/{Version}/swagger.json");
            });

            _ = app.UseSwagger(options =>
           {
               options.RouteTemplate = "swagger/swagger/{documentname}/swagger.json";
           });

            _ = app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = Title;
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint($"swagger/{Version}/swagger.json", Description);
            });
        }
    }
}
