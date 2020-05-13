using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    /// <summary>
    /// Command to process username and send message with invitation.
    /// </summary>
    public class UsernameCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = "@";

        /// <summary>
        /// Process username and send message with invitation.
        /// </summary>
        /// <param name="message">Telegram message.</param>
        /// <param name="client">Telegram client.</param>
        /// <param name="userManager">Manager of application users.</param>
        /// <returns></returns>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            Console.WriteLine("Execution of username command!");

            // Sender parameters.
            var senderId = message.Chat.Id;
            var senderUsername = message.Chat.Username;

            var recipientUsername = message.Text.Replace("@","");
            Console.WriteLine($"Recipient username: {recipientUsername}.");

            var recipient = await userManager.GetUserByUsernameAsync(recipientUsername);

            if (recipient == null)
            {
                Console.WriteLine($"Unknown or not registered user {recipientUsername}! Try again please!");
                await client.SendTextMessageAsync(senderId, $"@{recipientUsername} is not a member of CoffeeBot! Try again please!");
                return;
            }

            // Success.
            var recipientId = recipient.Id;
            Console.WriteLine($"Send invitation to battle to {recipientUsername}.");
            await client.SendTextMessageAsync(recipientId, $"Are you ready for coffee battle with @{senderUsername}?");
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}