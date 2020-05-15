using System.Threading.Tasks;
using Core.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface for telegram callback executing.
    /// </summary>
    public interface ICallbackHandler
    {
        /// <summary>
        /// Execute telegram callback.
        /// </summary>
        /// <param name="callbackQuery">Callback query.</param>
        /// <param name="client">TelegramBot client interface.</param>
        /// <param name="userManager">Manager of application users.</param>
        Task<Result> Execute(CallbackQuery callbackQuery, ITelegramBotClient client, IUserManager userManager);
    }
}