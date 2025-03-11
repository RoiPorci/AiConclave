using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

public abstract class CreateFactionTestBase
{
    protected readonly Mock<IFactionRepository> FactionRepositoryMock = new();
    protected readonly TestPresenter<CreateFactionResponse> Presenter = new();
    protected readonly CreateFaction  CreateFaction;

    protected CreateFactionTestBase()
    {
        if (FactionRepositoryMock == null)
            throw new Exception("FactionRepositoryMock est NULL ! Vérifie son initialisation.");

        
        var createFactionRuleChecker = 
            new CreateFactionRuleChecker(FactionRepositoryMock.Object);
        
        CreateFaction = new CreateFaction(
            FactionRepositoryMock.Object, 
            new FactionRuleChecker(), 
            createFactionRuleChecker
            );
    }

    protected async Task ExecuteRequestAsync(CreateFactionRequest request)
    {
        await CreateFaction.ExecuteAsync(request, Presenter);
    }
    
    protected void AssertError(string expectedError)
    {
        var response = Presenter.GetResponse();
        
        // Assert operation failed and the expected error occurred
        Assert.False(response.IsSuccess);
        Assert.Contains(expectedError, response.Errors);
        
        // Assert data is null
        Assert.Null(response.FactionId);
        Assert.Null(response.Code);
        Assert.Null(response.Name);
        Assert.Null(response.Description);
        
    }
}