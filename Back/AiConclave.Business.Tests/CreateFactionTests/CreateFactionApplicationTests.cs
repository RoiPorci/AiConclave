using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
/// Unit tests for validating the application logic when creating a faction.
/// Ensures that uniqueness constraints are properly enforced.
/// </summary>
public class CreateFactionApplicationTests  : CreateFactionTestBase
{
    /// <summary>
    /// Ensures that an error is returned when the faction code is not unique.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenCodeIsNotUnique()
    {
        const string duplicateCode = "duplicateCode";
        var request = CreateFactionCommandBuilder.WithCode(duplicateCode).Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(duplicateCode))
            .ReturnsAsync(true);
        
        await ExecuteUseCaseAsync(request);
        
        AssertError("Code already exists.");
    }
    
    /// <summary>
    /// Ensures that an error is returned when the faction name is not unique.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenNameIsNotUnique()
    {
        var duplicateName = "duplicateName";
        var request = CreateFactionCommandBuilder.WithName(duplicateName).Build();
        
        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(duplicateName))
            .ReturnsAsync(true);
        
        await ExecuteUseCaseAsync(request);
        
        AssertError("Name already exists.");
    }
}