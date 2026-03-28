using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EventBooking.Web.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ApiService(
        HttpClient httpClient,
        IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;

        var baseUrl =
            _config["ApiSettings:BaseUrl"];

        _httpClient.BaseAddress =
            new Uri(baseUrl);
    }

    public async Task<List<T>>
        GetAsync<T>(string url, string token = null)
    {
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }

        var response =
            await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content
                .ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<T>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }

    public async Task<HttpResponseMessage>
        PostAsync<T>(
        string url,
        T data,
        string token = null)
    {
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }

        var json =
            JsonSerializer.Serialize(data);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        return await _httpClient.PostAsync(
            url,
            content);
    }

    public async Task<HttpResponseMessage>
    DeleteAsync(
    string url,
    string token)
{
    _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue(
            "Bearer",
            token);

    return await _httpClient.DeleteAsync(url);
}
}