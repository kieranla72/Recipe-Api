using DB.Daos;
using DB.Models;
using Lib.Services;
using Moq;

namespace LibTest.Services;

public class GameServiceTest
{
    
    [Fact]
    public async void TryGetCache_MultipleCallsOnlyResultsInOneDataRetrieval()
    {
        var gameService = SetUpService();
        var x = await gameService.SaveSportsParallel(new List<GameCreateDto>());
        Assert.Equal(2, x);


    }    
    
    [Fact]
    public async void TryGetCache_MultipleCallsOnlyResultsInOneDataRetrievalNonParallel()
    {
        var gameService = SetUpService();
        var x = await gameService.SaveSportsNonParallel(new List<GameCreateDto>());
        Assert.Equal(2, x);

    }    
    [Fact]
    public async void TryGetCache_MultipleCallsOnlyResultsInOneDataRetrievalParallelPartition()
    {
        var gameService = SetUpService();
        var x = await gameService.SaveSportsParallelListPartition(new List<GameCreateDto>());
        Assert.Equal(2, x);

    }    
    [Fact]
    public async void TryGetCache_MultipleCallsOnlyResultsInOneDataRetrievalNonParallelPartition()
    {
        var gameService = SetUpService();
        var x = await gameService.SaveSportsNonParallelListPartition(new List<GameCreateDto>());
        Assert.Equal(2, x);

    }
    
    
    private GamesService SetUpService()
    {
        return new GamesService(new Mock<IGamesDao>().Object);
    }
}