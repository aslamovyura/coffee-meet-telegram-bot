using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Core.Resources;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Core.Commands
{
    /// <summary>
    /// Command to invite user to coffee battle.
    /// </summary>
    public abstract class InviteCommand
    {
        /// <summary>
        /// Send invitation for coffee battle.
        /// </summary>
        /// <param name="recipient">Recipient user.</param>
        /// <param name="sender">Sender user.</param>
        /// <param name="bot">Telegram bot cliet.</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Invite(AppUser sender, AppUser recipient, ITelegramBotClient bot)
        {
            sender = sender ?? throw new ArgumentNullException(nameof(sender));
            recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            bot = bot ?? throw new ArgumentNullException(nameof(bot));

            var accept = JsonConvert.SerializeObject(new InvitationResponce { Answer = Answer.Accept, ToId = sender.Id, FromId = recipient.Id });
            var decline = JsonConvert.SerializeObject(new InvitationResponce { Answer = Answer.Decline, ToId = sender.Id, FromId = recipient.Id });

            var keyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithCallbackData(InviteCommon.Accept, accept),
                    InlineKeyboardButton.WithCallbackData(InviteCommon.Decline, decline),
                });

            var recipientText = string.Format(InviteCommon.AreYouReady, sender.Username);
            await bot.SendTextMessageAsync(recipient.Id, $"\ud83d\udd14 {recipientText} \ud83e\udd20", ParseMode.Default, false, false, 0, keyboard);

            var senderText = string.Format(InviteCommon.InvitationSent, recipient.Username);
            await bot.SendTextMessageAsync(sender.Id, $"{senderText} \ud83d\udc4c {InviteCommon.Wait}");
        }

        /// <summary>
        /// Get user parameters from Telegram message.
        /// </summary>
        /// <param name="message">Telegram message.</param>
        /// <param name="userManager"Manager of application users.></param>
        /// <returns>Sender user.</returns>
        public async Task<AppUser> GetSender(Message message, IUserManager userManager)
        {
            // Sender parameters.
            var senderId = message.Chat.Id;
            var senderUsername = message.Chat.Username;
            var sender = await userManager.GetUserByUsernameAsync(senderUsername);

            if (sender == null)
            {
                await userManager.CreateUserAsync(senderId, senderUsername, null, null);
            }

            return sender;
        }
    }
}