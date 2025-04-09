using AiConclave.Business.Application.Factions;

namespace AiConclave.Business.Tests.ModifyFactionInfosTests.Helpers
{
    /// <summary>
    /// Builder class for creating instances of <see cref="ModifyFactionInfosCommand"/> for testing purposes.
    /// Provides fluent methods to customize command fields.
    /// </summary>
    public class ModifyFactionInfosCommandBuilder
    {
        private readonly ModifyFactionInfosCommand _command;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyFactionInfosCommandBuilder"/> class with default values.
        /// </summary>
        /// <param name="presenter">The test presenter that will receive the response.</param>
        public ModifyFactionInfosCommandBuilder(TestPresenter<ModifyFactionInfosResponse> presenter)
        {
            _command = new ModifyFactionInfosCommand
            (
                Guid.NewGuid(),
                "TST",              // Default code
                "TestName",         // Default name
                "TestDescription",  // Default description
                presenter
            );
        }

        /// <summary>
        /// Sets the faction ID in the command.
        /// </summary>
        /// <param name="factionId">The unique identifier of the faction.</param>
        /// <returns>The current <see cref="ModifyFactionInfosCommandBuilder"/> instance.</returns>
        public ModifyFactionInfosCommandBuilder WithFactionId(Guid factionId)
        {
            _command.FactionId = factionId;
            return this;
        }

        /// <summary>
        /// Sets the faction code in the command.
        /// </summary>
        /// <param name="code">The new code for the faction.</param>
        /// <returns>The current <see cref="ModifyFactionInfosCommandBuilder"/> instance.</returns>
        public ModifyFactionInfosCommandBuilder WithCode(string code)
        {
            _command.Code = code;
            return this;
        }

        /// <summary>
        /// Sets the faction name in the command.
        /// </summary>
        /// <param name="name">The new name for the faction.</param>
        /// <returns>The current <see cref="ModifyFactionInfosCommandBuilder"/> instance.</returns>
        public ModifyFactionInfosCommandBuilder WithName(string name)
        {
            _command.Name = name;
            return this;
        }

        /// <summary>
        /// Sets the faction description in the command.
        /// </summary>
        /// <param name="description">The new description for the faction.</param>
        /// <returns>The current <see cref="ModifyFactionInfosCommandBuilder"/> instance.</returns>
        public ModifyFactionInfosCommandBuilder WithDescription(string description)
        {
            _command.Description = description;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured <see cref="ModifyFactionInfosCommand"/>.
        /// </summary>
        /// <returns>The constructed command instance.</returns>
        public ModifyFactionInfosCommand Build()
        {
            return _command;
        }
    }
}
