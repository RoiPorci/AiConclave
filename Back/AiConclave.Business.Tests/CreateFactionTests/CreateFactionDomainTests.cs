using AiConclave.Business.Tests.CreateFactionTests.Helpers;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
///     Unit tests for validating the domain rules when creating a faction.
///     Ensures that required fields are properly validated.
/// </summary>
public class CreateFactionDomainTests : CreateFactionTestBase
{
    /// <summary>
    ///     Ensures that an error is returned when the faction code is <see langword="null" />.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenCodeIsNull()
    {
        var request = CreateFactionCommandBuilder.WithCode(null!).Build();

        await ExecuteUseCaseAsync(request);

        AssertError("Faction code is required.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction name is <see langword="null" />.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenNameIsNull()
    {
        var request = CreateFactionCommandBuilder.WithName(null!).Build();

        await ExecuteUseCaseAsync(request);

        AssertError("Faction name is required.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction description is <see langword="null" />.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenDescriptionIsNull()
    {
        var request = CreateFactionCommandBuilder.WithDescription(null!).Build();

        await ExecuteUseCaseAsync(request);

        AssertError("Faction description is required.");
    }
}