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
        public string Key { get; } = CommonConstants.StartKey;

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, $"Hello {message.From.Username}!");

            var username = message.Chat.Username ?? chatId.ToString();

            var result = await userManager.CreateUserAsync(chatId, username, null, null);

            if (result == null)
            {
                Console.WriteLine("User is already exist!");
                return;
            }

            if (result.Succeeded)
            {
                Console.WriteLine("User was successfully added!");
                return;
            }
            else
            {
                foreach (var error in result.Errors)
                    Console.WriteLine($"-------> ERROR: {error} ");
            }
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}