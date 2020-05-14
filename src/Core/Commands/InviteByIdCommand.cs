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
    public class InviteByIdCommand : InviteCommand, ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = string.Empty;

        /// <summary>
        /// Process username and send message with invitation.
        /// </summary>
        /// <param name="message">Telegram message.</param>
        /// <param name="client">Telegram client.</param>
        /// <param name="userManager">Manager of application users.</param>
        /// <returns></returns>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            // Sender parameters.
            var sender = await GetSender(message, userManager);

            // Recipient parameters.
            var success = long.TryParse(message.Text.Trim(), out var recipientId);
            if (!success)
            {
                await client.SendTextMessageAsync(sender.Id, $"{message.Text} is not an user Id! Try again please!");
                return;
            }

            var recipient = await userManager.GetUserByIdAsync(recipientId);
            if (recipient == null)
            {
                await client.SendTextMessageAsync(sender.Id, $"User with Id {recipientId} is not a member of CoffeeBot! Try again please!");
                return;
            }

            // Send invitation.
            await Invite(sender, recipient, client);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : true;
    }
}