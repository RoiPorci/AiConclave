using AiConclave.Business.Application.Factions.OwnedResources;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using Moq;

namespace AiConclave.Business.Tests.InitFactionResourcesTests.Helpers;

/// <summary>
/// Base class for SetFactionInitialResources tests.
/// Provides common setup and helper methods for test execution and validation.
/// </summary>
public abstract class InitFactionResourcesTestBase
{
    /// <summary>
    /// Mock repository used to simulate data access for factions.
    /// </summary>
    protected readonly Mock<IFactionRepository> FactionRepositoryMock = new();

    /// <summary>
    /// Builder used to construct <see cref="InitFactionResourcesCommand"/> instances for testing.
    /// </summary>
    protected InitFactionResourcesCommandBuilder CommandBuilder =>
        new InitFactionResourcesCommandBuilder(_presenter);

    private readonly TestPresenter<InitFactionResourcesResponse> _presenter = new();

    private readonly InitFactionResourcesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="InitFactionResourcesTestBase"/> class.
    /// Sets up the handler with mocked dependencies and rule checkers.
    /// </summary>
    protected InitFactionResourcesTestBase()
    {
        _handler = new InitFactionResourcesHandler(
            FactionRepositoryMock.Object,
            new InitFactionResourcesRuleChecker(),
            new FactionResourcesRuleChecker()
        );
    }

    /// <summary>
    /// Creates a new <see cref="Faction"/> instance with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the faction.</param>
    /// <param name="code">The faction code (default is "TST").</param>
    /// <param name="name">The faction name (default is "TestName").</param>
    /// <param name="description">The faction description (default is "TestDescription").</param>
    /// <returns>A new <see cref="Faction"/> instance.</returns>
    protected static Faction CreateFaction
    (
        Guid id, 
        string code = "TST", 
        string name = "TestName", 
        string description = "TestDescription"
    )
    {
        var faction = Faction.Create(code, name, description);
        faction.Id = id;
        return faction;
    }

    /// <summary>
    /// Executes the use case handler for the given command.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    protected async Task ExecuteUseCaseAsync(InitFactionResourcesCommand command)
    {
        await _handler.Handle(command, CancellationToken.None);
    }

    /// <summary>
    /// Asserts that the response contains a specific error and no updated resources.
    /// </summary>
    /// <param name="expectedError">The expected error message.</param>
    protected void AssertError(string expectedError)
    {
        var response = _presenter.GetResponse();

        Assert.False(response.IsSuccess);
        Assert.Contains(expectedError, response.Errors);
        Assert.Empty(response.UpdatedResources);
    }

    /// <summary>
    /// Asserts that the response is successful and the updated resources match the expected values.
    /// </summary>
    /// <param name="command">The command used to generate the response.</param>
    protected void AssertNoErrors(InitFactionResourcesCommand command)
    {
        var response = _presenter.GetResponse();

        Assert.True(response.IsSuccess);
        Assert.Equal(command.FactionId, response.FactionId);
        Assert.Equal(command.ResourceAmounts.Count, response.UpdatedResources.Count);

        foreach (var expected in command.ResourceAmounts)
        {
            var actual = response.UpdatedResources.FirstOrDefault(r => r.ResourceCode == expected.ResourceCode);
            Assert.NotNull(actual);
            Assert.Equal(expected.Amount, actual!.Amount);
        }
    }
}
