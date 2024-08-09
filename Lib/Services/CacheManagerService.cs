using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.Caching.Memory;

namespace Lib.Services;

public enum CacheKeys {
    IngredientsData,
    RecipesData
}

public class CacheManagerService : ICacheManagerService
{
    private MemoryCache _cache { get; } = new (
        new MemoryCacheOptions
        {
            SizeLimit = 1024
        });
    
    public async Task<TData> TryGetCache<TData>(CacheKeys cacheKey, double timeSpan, Func<Task<TData>> dataRetrievalFunction)
    {
        TData data;
        if (_cache.TryGetValue(cacheKey, out data))
        {
            return data;
        };
        
        data = await dataRetrievalFunction();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(timeSpan))
            .SetSize(1);
        _cache.Set(cacheKey, data, cacheEntryOptions);

        return data;
    }
}