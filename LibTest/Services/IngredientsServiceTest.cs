using DB.Daos;
using DB.Models;
using Lib.Services;
using Moq;

namespace LibTest.Services;

public class IngredientsServiceTest
{
    private IngredientsService SetUpService()
    {
        return new IngredientsService(new Mock<IIngredientsDao>().Object);
    }
}