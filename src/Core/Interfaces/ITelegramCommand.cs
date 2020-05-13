using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Core.Interfaces
{
    /// <summary>
    /// Telegram command.
    /// </summary>
    public interface ITelegramCommand
    {
        /// <summary>
        /// Command key.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="client">TelegramBot client interface.</param>
        Task Execute(Message message, ITelegramBotClient client, IUserManager userManager);

        /// <summary>
        /// Find a command key in text.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Operation result.</returns>
        bool Contains(Message message);
    }
}