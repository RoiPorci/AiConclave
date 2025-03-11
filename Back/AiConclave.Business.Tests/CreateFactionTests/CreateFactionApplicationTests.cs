using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

public class CreateFactionApplicationTests  : CreateFactionTestBase
{
    [Fact]
    public async Task ShouldReturnError_WhenCodeIsNotUnique()
    {
        string duplicateCode = "duplicateCode";
        var request = new CreateFactionRequestBuilder().WithCode(duplicateCode).Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(duplicateCode))
            .ReturnsAsync(true);
        
        await ExecuteRequestAsync(request);
        
        AssertError("Code already exists.");
    }
    
    [Fact]
    public async Task ShouldReturnError_WhenNameIsNotUnique()
    {
        string duplicateName = "duplicateName";
        var request = new CreateFactionRequestBuilder().WithName(duplicateName).Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(duplicateName))
            .ReturnsAsync(true);
        
        await ExecuteRequestAsync(request);
        
        AssertError("Name already exists.");
    }
}