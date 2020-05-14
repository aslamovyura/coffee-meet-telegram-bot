using System.Collections.Generic;
using Core.Commands;
using Core.Interfaces;

namespace Core.Services
{
    /// <inheritdoc cref="ICommandService"/>
    public class CommandService : ICommandService
    {
        private readonly IEnumerable<ITelegramCommand> _commands;

        /// <summary>
        /// Base constructor.
        /// </summary>
        public CommandService()
        {
            _commands = new List<ITelegramCommand>
            {
                new StartCommand(),
                new InviteByContactCommand(),
                new InviteByUsernameCommand(),
                new InviteByRandomCommand(),
                new AboutCommand(),
                new InviteByIdCommand(),
            };
        }

        /// <inheritdoc/>
        public IEnumerable<ITelegramCommand> Get() => _commands;
    }
}