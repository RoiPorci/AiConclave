using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using Moq;

namespace AiConclave.Business.Tests.ModifyFactionInfosTests.Helpers
{
    /// <summary>
    /// Base class for tests related to modifying faction information.
    /// Provides setup, mocks, builders, and assertion helpers.
    /// </summary>
    public abstract class ModifyFactionInfosTestBase
    {
        private readonly ModifyFactionInfosHandler _modifyFactionHandler;
        private readonly TestPresenter<ModifyFactionInfosResponse> _presenter = new();

        /// <summary>
        /// Gets the mocked faction repository used to stub and verify interactions.
        /// </summary>
        protected readonly Mock<IFactionRepository> FactionRepositoryMock = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyFactionInfosTestBase"/> class.
        /// Sets up the handler and validation rule checkers.
        /// </summary>
        protected ModifyFactionInfosTestBase()
        {
            var modifyFactionInfosRuleChecker = new ModifyFactionInfosRuleChecker(FactionRepositoryMock.Object);

            _modifyFactionHandler = new ModifyFactionInfosHandler(
                modifyFactionInfosRuleChecker,
                FactionRepositoryMock.Object,
                new FactionRuleChecker()
            );
        }

        /// <summary>
        /// Gets a command builder to fluently construct <see cref="ModifyFactionInfosCommand"/> instances.
        /// </summary>
        protected ModifyFactionInfosCommandBuilder ModifyFactionInfosCommandBuilder => new(_presenter);

        /// <summary>
        /// Executes the use case with the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        protected async Task ExecuteUseCaseAsync(ModifyFactionInfosCommand command)
        {
            await _modifyFactionHandler.Handle(command, CancellationToken.None);
        }

        /// <summary>
        /// Asserts that the response contains the expected error message.
        /// </summary>
        /// <param name="expectedError">The error message expected in the response.</param>
        protected void AssertError(string expectedError)
        {
            var response = _presenter.GetResponse();
            Assert.False(response.IsSuccess);
            Assert.Contains(expectedError, response.Errors);
        }

        /// <summary>
        /// Asserts that the response contains no errors and matches the modified values.
        /// </summary>
        /// <param name="request">The request that was used to perform the update.</param>
        protected void AssertNoErrors(ModifyFactionInfosCommand request)
        {
            var response = _presenter.GetResponse();
            Assert.True(response.IsSuccess);
            Assert.Equal(request.FactionId, response.FactionId);
            Assert.Equal(request.Code, response.Code);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Description, response.Description);
        }
    }
}
