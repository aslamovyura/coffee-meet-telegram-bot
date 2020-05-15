using System.Threading.Tasks;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    /// <summary>
    /// Command to process user contact and send message with invitation.
    /// </summary>
    public class InviteByContactCommand : InviteCommand, ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = string.Empty;

        /// <summary>
        /// Process user contact and send message with invitation.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            // Sender parameters.
            var sender = await GetSender(message, userManager);

            // Recipient parameters.
            var recepientId = message.Contact.UserId;
            var recipient = await userManager.GetUserByIdAsync(recepientId);

            if (recipient == null)
            {
                await client.SendTextMessageAsync(sender.Id, $"@{recipient.Username} is not a member of CoffeeBot! Try again please!");
                return;
            }

            // Send invitation.
            await Invite(sender, recipient, client);
        }

        public bool Contains(Message message) => message.Type != MessageType.Contact ? false : true;
    }
}