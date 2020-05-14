using System;
using System.Threading.Tasks;
using Core.Common;
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
            await client.SendTextMessageAsync(chatId, $"Hello @{message.From.Username} and Welcome! This telegram bot allows you to send invitation for coffee meeting to different users. You may choose a certain user (enter @username or share contact with me), or random registered user (enter: /random). For more project details enter: /about. Enjoy!");

            var username = message.Chat.Username ?? chatId.ToString();
            await userManager.CreateUserAsync(chatId, username, null, null);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}