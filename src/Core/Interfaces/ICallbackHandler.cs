using System.Threading.Tasks;
using Core.Common;
using Telegram.Bot;

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
        /// <param name="callbackData">Callback data.</param>
        /// <param name="client">TelegramBot client interface.</param>
        /// <param name="userManager">Manager of application users.</param>
        Task<Result> Execute(string callbackData, ITelegramBotClient client, IUserManager userManager);
    }
}