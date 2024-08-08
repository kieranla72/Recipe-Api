using Lib.Services;

namespace LibTest.Services;

public class CacheManagerServiceTest
{
    [Fact]
    public async void TryGetCache_MultipleCallsOnlyResultsInOneDataRetrieval()
    {
        var service = SetUpService();
        const string fakeData = "Fake data";

        var numberOfDataRetrievalCalls = 0;
        var dataRetrieval = async () =>
        {
            numberOfDataRetrievalCalls++;
            return fakeData;
        };

        await service.TryGetCache(CacheKeys.RecipesData, 10, dataRetrieval);
        await service.TryGetCache(CacheKeys.RecipesData, 10, dataRetrieval);
        await service.TryGetCache(CacheKeys.RecipesData, 10, dataRetrieval);
        var result = await service.TryGetCache(CacheKeys.RecipesData, 10, dataRetrieval);
        
        Assert.Equal(1, numberOfDataRetrievalCalls);
        Assert.Equal(fakeData, result);
    }
    
    [Fact]
    public async void TryGetCache_DataExpiringMeansDataRetrievalIsCalledAgain()
    {
        var service = SetUpService();
        const string fakeData = "Fake data";

        var numberOfDataRetrievalCalls = 0;
        var dataRetrieval = async () =>
        {
            numberOfDataRetrievalCalls++;
            return fakeData;
        };

        await service.TryGetCache(CacheKeys.RecipesData, 0.001, dataRetrieval);
        await service.TryGetCache(CacheKeys.RecipesData, 0.001, dataRetrieval);
        Thread.Sleep(10);
        await service.TryGetCache(CacheKeys.RecipesData, 0.001, dataRetrieval);
        
        Assert.Equal(2, numberOfDataRetrievalCalls);
    }    
    
    private CacheManagerService SetUpService()
    {
        return new CacheManagerService();
    }
}