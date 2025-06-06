using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

/// <summary>
///     Base class for faction creation tests.
///     Provides common setup and helper methods for test execution and validation.
/// </summary>
public abstract class CreateFactionTestBase
{
    /// <summary>
    ///     The use case instance for creating a faction.
    /// </summary>
    private readonly CreateFactionHandler _createFactionHandler;

    /// <summary>
    ///     Presenter used to capture the response of the use case.
    /// </summary>
    private readonly TestPresenter<CreateFactionResponse> _presenter = new();

    /// <summary>
    ///     Mock repository for managing factions.
    /// </summary>
    protected readonly Mock<IFactionRepository> FactionRepositoryMock = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionTestBase" /> class.
    ///     Sets up dependencies and prepares the test environment.
    /// </summary>
    protected CreateFactionTestBase()
    {
        var createFactionRuleChecker =
            new CreateFactionRuleChecker(FactionRepositoryMock.Object);

        _createFactionHandler = new CreateFactionHandler(
            FactionRepositoryMock.Object,
            new FactionRuleChecker(),
            createFactionRuleChecker
        );
    }

    protected CreateFactionCommandBuilder CreateFactionCommandBuilder => new(_presenter);

    /// <summary>
    ///     Executes the faction creation use case with the given command.
    /// </summary>
    /// <param name="command">The command containing faction details.</param>
    protected async Task ExecuteUseCaseAsync(CreateFactionCommand command)
    {
        await _createFactionHandler.Handle(command, CancellationToken.None);
    }

    /// <summary>
    ///     Asserts that the response contains the expected error and no valid faction data.
    /// </summary>
    /// <param name="expectedError">The expected error message.</param>
    protected void AssertError(string expectedError)
    {
        var response = _presenter.GetResponse();

        // Assert operation failed and the expected error occurred
        Assert.False(response.IsSuccess);
        Assert.Contains(expectedError, response.Errors);

        // Assert data is null
        Assert.Null(response.FactionId);
        Assert.Null(response.Code);
        Assert.Null(response.Name);
        Assert.Null(response.Description);
        Assert.Empty(response.InitialResources);
    }

    protected void AssertNoErrors(CreateFactionCommand command)
    {
        var response = _presenter.GetResponse();

        // Assert operation succeeded 
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.FactionId);

        // Assert data corresponds to command
        Assert.Equal(command.Code, response.Code);
        Assert.Equal(command.Name, response.Name);
        Assert.Equal(command.Description, response.Description);

        // Assert resources are correctly set
        Assert.Equal(command.ResourceAmounts.Count, response.InitialResources.Count);
        foreach (var expected in command.ResourceAmounts)
        {
            var actual = response.InitialResources.FirstOrDefault(r => r.ResourceCode == expected.ResourceCode);
            Assert.NotNull(actual);
            Assert.Equal(expected.Amount, actual.Amount);
        }
    }

    /// <summary>
    ///     Asserts that all faction resources are initialized and their amounts are zero.
    /// </summary>
    /// <param name="faction">The faction to check.</param>
    protected static void AssertResourcesInitializedToZero(Faction faction)
    {
        Assert.NotNull(faction.OwnedResources);
        Assert.Equal(Resource.All.Count, faction.OwnedResources.Count);

        foreach (var resource in Resource.All)
        {
            Assert.True(faction.OwnedResources.ContainsKey(resource.Code),
                $"Resource '{resource.Code}' is missing.");

            Assert.Equal(0, faction.OwnedResources[resource.Code].Amount);
        }
    }
}