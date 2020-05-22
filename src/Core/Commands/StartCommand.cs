using System;
using System.Threading.Tasks;
using Core.Constants;
using Core.Interfaces;
using Core.Resources;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    /// <summary>
    /// Start command for telegram bot.
    /// </summary>
    public class StartCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = CommandConstants.StartKey;

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            Console.WriteLine("Start command!");

            var chatId = message.Chat.Id;
            var username = message.Chat.Username ?? chatId.ToString();

            var helloMessage = string.Format(Start.Hello, username);
            await client.SendTextMessageAsync(chatId, $"{helloMessage} \ud83e\udd73 \ud83c\udf89 {Start.AllowToSendInvitation} \u2615\ufe0f \ud83c\udf69 {Start.MainCommands}");
            await userManager.CreateUserAsync(chatId, username, null, null);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}