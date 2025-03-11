using AiConclave.Business.Tests.CreateFactionTests.Helpers;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
/// Unit tests for validating the domain rules when creating a faction.
/// Ensures that required fields are properly validated.
/// </summary>
public class CreateFactionDomainTests : CreateFactionTestBase
{
    /// <summary>
    /// Ensures that an error is returned when the faction code is <see langword="null"/>.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenCodeIsNull()
    {
        var request = new CreateFactionRequestBuilder().WithCode(null!).Build();
        
        await ExecuteRequestAsync(request);
        
        AssertError("Faction code is required.");
    }
    
    /// <summary>
    /// Ensures that an error is returned when the faction name is <see langword="null"/>.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenNameIsNull()
    {
        var request = new CreateFactionRequestBuilder().WithName(null!).Build();
        
        await ExecuteRequestAsync(request);
        
        AssertError("Faction name is required.");
    }
    
    /// <summary>
    /// Ensures that an error is returned when the faction description is <see langword="null"/>.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenDescriptionIsNull()
    {
        var request = new CreateFactionRequestBuilder().WithDescription(null!).Build();
        
        await ExecuteRequestAsync(request);
        
        AssertError("Faction description is required.");
    }
}