namespace Lib.Services;

public interface ICacheManagerService
{

    public Task<TData> TryGetCache<TData>(CacheKeys cacheKey, double timeSpan, Func<Task<TData>> dataRetrievalFunction);
}