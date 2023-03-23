namespace UpSwot.Core;

public interface IRickAndMortyClient
{
    Task<T?> GetDataAsync<T>(string name, string method);

    Task<T?> GetNextPageAsync<T>(string nextPath);
}