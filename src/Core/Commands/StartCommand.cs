using System;
using System.Threading.Tasks;
using Core.Constants;
using Core.Interfaces;
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

            await client.SendTextMessageAsync(chatId, $"Hello @{username} and Welcome! \ud83e\udd73 \ud83c\udf89 This telegram bot allows you to send invitation for coffee meeting to different users. \u2615\ufe0f \ud83c\udf69 You may choose a certain user (enter @username or share contact with me), or random registered user (enter: /random). For more project details enter: /about. Enjoy!");
            await userManager.CreateUserAsync(chatId, username, null, null);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}