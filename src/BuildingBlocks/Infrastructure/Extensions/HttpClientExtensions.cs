using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage httpResponse)
    {
        if (!httpResponse.IsSuccessStatusCode)
            throw new ArgumentNullException($"Something went wrong calling the API: {httpResponse.ReasonPhrase}");
        
        var dataAstring = await httpResponse.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAstring, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        });
    }

    public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient client, string url, T data) 
    {
        var dataAstring = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAstring);

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return client.PostAsJsonAsync(url, content);
    }
}
