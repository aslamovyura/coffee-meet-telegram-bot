using System.Threading.Tasks;
using Core.Interfaces;
using Core.Resources;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    /// <summary>
    /// Unidentified command for telegram bot.
    /// </summary>
    public class UnknownCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = string.Empty;

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, $"{Unknown.UnknownCommand} \ud83e\udd14 {Unknown.TryAgain}");
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : true;
    }
}