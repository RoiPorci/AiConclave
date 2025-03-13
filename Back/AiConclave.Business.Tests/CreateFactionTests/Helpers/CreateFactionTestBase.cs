using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

/// <summary>
/// Base class for faction creation tests.
/// Provides common setup and helper methods for test execution and validation.
/// </summary>
public abstract class CreateFactionTestBase
{
    /// <summary>
    /// Mock repository for managing factions.
    /// </summary>
    protected readonly Mock<IFactionRepository> FactionRepositoryMock = new();

    /// <summary>
    /// Presenter used to capture the response of the use case.
    /// </summary>
    protected readonly TestPresenter<CreateFactionResponse> Presenter = new();

    /// <summary>
    /// The use case instance for creating a faction.
    /// </summary>
    protected readonly CreateFactionHandler CreateFactionHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFactionTestBase"/> class.
    /// Sets up dependencies and prepares the test environment.
    /// </summary>
    protected CreateFactionTestBase()
    {
        var createFactionRuleChecker = 
            new CreateFactionRuleChecker(FactionRepositoryMock.Object);
        
        CreateFactionHandler = new CreateFactionHandler(
            FactionRepositoryMock.Object, 
            new FactionRuleChecker(), 
            createFactionRuleChecker
        );
    }

    /// <summary>
    /// Executes the faction creation use case with the given command.
    /// </summary>
    /// <param name="command">The command containing faction details.</param>
    protected async Task ExecuteUseCaseAsync(CreateFactionCommand command)
    {
        await CreateFactionHandler.Handle(command, CancellationToken.None);
    }

    /// <summary>
    /// Asserts that the response contains the expected error and no valid faction data.
    /// </summary>
    /// <param name="expectedError">The expected error message.</param>
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
