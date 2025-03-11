using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests; 

public class CreateFactionDomainTests : CreateFactionTestBase
{
    [Fact]
    public async Task ShouldReturnError_WhenCodeIsNull()
    {
        var request = new CreateFactionRequestBuilder().WithCode(null).Build();
        
        await ExecuteRequestAsync(request);
        
        AssertError("Faction code is required.");
    }
    
    [Fact]
    public async Task ShouldReturnError_WhenNameIsNull()
    {
        var request = new CreateFactionRequestBuilder().WithName(null).Build();
        
        await ExecuteRequestAsync(request);
        
        AssertError("Faction name is required.");
    }
    
    [Fact]
    public async Task ShouldReturnError_WhenDeescriptionIsNull()
    {
        var request = new CreateFactionRequestBuilder().WithDescription(null).Build();
        
        await ExecuteRequestAsync(request);
        
        AssertError("Faction description is required.");
    }
}