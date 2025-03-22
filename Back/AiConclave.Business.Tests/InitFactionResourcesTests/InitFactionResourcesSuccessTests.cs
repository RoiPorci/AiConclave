using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Tests.InitFactionResourcesTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.InitFactionResourcesTests;

/// <summary>
///     Tests for successful execution of the SetFactionInitialResources use case.
/// </summary>
public class InitFactionResourcesSuccessTests : InitFactionResourcesTestBase
{
    /// <summary>
    ///     Ensures that the use case returns success when all validations pass.
    /// </summary>
    [Fact]
    public async Task ShouldReturnSuccess_WhenAllValidationsPass()
    {
        var command = CommandBuilder.Build();
        var faction = CreateFaction(command.FactionId, "WIN", "Winners", "We always win");

        FactionRepositoryMock
            .Setup(r => r.GetByIdAsync(command.FactionId))
            .ReturnsAsync(faction);

        FactionRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Faction>()))
            .Returns(Task.CompletedTask);

        await ExecuteUseCaseAsync(command);

        AssertNoErrors(command);
    }
}