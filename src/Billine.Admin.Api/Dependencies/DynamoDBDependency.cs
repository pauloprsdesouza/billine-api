using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Billine.Admin.Infrastructure.Database.Converters;
using EfficientDynamoDb;
using EfficientDynamoDb.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api.Dependencies
{
    public static class DynamoDBDependency
    {
        public static void AddDynamoDBDependency(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddAWSService<IAmazonDynamoDB>();

            var defaultCredentials = FallbackCredentialsFactory.GetCredentials();

            // If you need to access the actual keys:
            var accessKey = defaultCredentials.GetCredentials().AccessKey;
            var secretKey = defaultCredentials.GetCredentials().SecretKey;


            var credentials = new AwsCredentials(accessKey, secretKey);

            var config = new DynamoDbContextConfig(RegionEndpoint.USEast1, credentials)
            {
                Converters = new[] { new DateTimeOffsetConverter() }
            };

            var context = new DynamoDbContext(config);

            services.AddScoped<IDynamoDbContext, DynamoDbContext>(_ => context);
        }
    }
}
