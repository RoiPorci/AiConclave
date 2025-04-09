using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
///     Domain-level tests for validating business rules related to faction creation and resource initialization.
/// </summary>
public class CreateFactionDomainTests : CreateFactionTestBase
{


    /// <summary>
    ///     Ensures that an error is returned when a negative value is used for a non-negative resource.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenNegativeValueIsUsedOnNonNegativeResource()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithResource(Resource.Research, -10)
            .WithResource(Resource.Co2, 20) // to keep the total equal to 60
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"Resource '{Resource.Research.Name}' cannot have a negative amount.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction code is invalid.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionCodeIsInvalid()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithCode("") // Invalid empty code
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Faction code is required.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction name is invalid.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionNameIsInvalid()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithName("") // Invalid empty name
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Faction name is required.");
    }
}