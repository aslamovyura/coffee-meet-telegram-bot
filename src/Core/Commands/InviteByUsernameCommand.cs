using System.Threading.Tasks;
using Core.Constants;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    /// <summary>
    /// Command to process username and send message with invitation.
    /// </summary>
    public class InviteByUsernameCommand : InviteCommand, ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = CommandConstants.UsernameKey;

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
            var recipientUsername = message.Text.Trim().Replace("@","");
            var recipient = await userManager.GetUserByUsernameAsync(recipientUsername);

            if (recipient == null)
            {
                await client.SendTextMessageAsync(sender.Id, $"@{recipientUsername} is not a member of CoffeeBot! \ud83d\ude2d Try again please!");
                return;
            }

            // Send invitation.
            await Invite(sender, recipient, client);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}