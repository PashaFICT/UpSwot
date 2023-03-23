using Microsoft.Extensions.Caching.Memory;

namespace UpSwot.Core
{
    public class RickAndMortyCachingDecorator : IRickAndMortyClient
    {
        private readonly IRickAndMortyClient _decoratedClient;
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private const int ExpirationInHours = 1;

        public RickAndMortyCachingDecorator(IRickAndMortyClient decoratedClient)
        {
            _decoratedClient = decoratedClient;
        }
        public async Task<T?> GetDataAsync<T>(string name, string method)
        {
            var key = name + method;
            if (_cache.TryGetValue(key, out T data))
            {
                return data;
            }
            data = await _decoratedClient.GetDataAsync<T>(name, method);
            return _cache.Set(key, data, new MemoryCacheEntryOptions()
                                 .SetSize(1)
                                 .SetAbsoluteExpiration(TimeSpan.FromHours(ExpirationInHours)));

        }
        public async Task<T?> GetNextPageAsync<T>(string nextPage)
        {
            if (_cache.TryGetValue(nextPage, out T data))
            {
                return data;
            }
            data = await _decoratedClient.GetNextPageAsync<T>(nextPage);
            return _cache.Set(nextPage, data, new MemoryCacheEntryOptions()
                                .SetSize(1)
                                .SetAbsoluteExpiration(TimeSpan.FromHours(ExpirationInHours)));
        }
    }
}
