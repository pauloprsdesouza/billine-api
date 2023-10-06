using System.Text.Json.Serialization;
using System.Text.Json;

namespace Billine.Admin.Api.Configurations
{
    public static class ApiJsonSerializerOptions
    {
        public static JsonSerializerOptions Default(this JsonSerializerOptions options)
        {
            options.PropertyNameCaseInsensitive = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }
    }
}