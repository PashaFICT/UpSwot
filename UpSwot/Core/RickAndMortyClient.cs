namespace UpSwot.Core;

public class RickAndMortyClient : IRickAndMortyClient
{
    private readonly HttpClient _client;
    private readonly string _baseAddress;

    public RickAndMortyClient(string baseAddress)
    {
        _client = new HttpClient();
        _baseAddress = baseAddress;
    }
    
    public async Task<T?> GetDataAsync<T>(string name, string method)
    {
        string url = $"{_baseAddress}/{method}?name={name}";
        var response = await _client.GetAsync(url);
        return await response.Content.ReadFromJsonAsync<T>();
    }
    
    public async Task<T?> GetNextPageAsync<T>(string nextPath)
    {
        var response = await _client.GetAsync(nextPath);
        return await response.Content.ReadFromJsonAsync<T>();
    }
}