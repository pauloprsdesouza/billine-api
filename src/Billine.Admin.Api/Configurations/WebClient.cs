using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Billine.Admin.Api.Configurations
{
    public static class WebClient
    {
        public static async Task<TResult> ReadAsJsonAsync<TResult>(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(json, new JsonSerializerOptions().Default());
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string requestUri, object content)
        {
            var jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions().Default());
            var jsonContentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return await client.PostAsync(requestUri, jsonContentString);
        }
    }
}
