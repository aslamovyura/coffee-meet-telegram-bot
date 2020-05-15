using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Core.Commands
{
    /// <summary>
    /// Command to invite user to coffee battle..
    /// </summary>
    public abstract class InviteCommand
    {
        /// <summary>
        /// Send invitation for coffee battle.
        /// </summary>
        /// <param name="recipient">Recipient user.</param>
        /// <param name="sender">Sender user.</param>
        /// <param name="bot">Telegram bot cliet.</param>
        /// <returns></returns>
        public async Task Invite(AppUser sender, AppUser recipient, ITelegramBotClient bot)
        {
            sender = sender ?? throw new ArgumentNullException(nameof(sender));
            recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            bot = bot ?? throw new ArgumentNullException(nameof(bot));

            var accept = JsonConvert.SerializeObject(new InvitationResponce { Answer = Answer.Accept, ToId = sender.Id, FromId = recipient.Id });
            var decline = JsonConvert.SerializeObject(new InvitationResponce { Answer = Answer.Decline, ToId = sender.Id, FromId = recipient.Id });

            var rkm = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithCallbackData("Accept", accept),
                    InlineKeyboardButton.WithCallbackData("Decline", decline),
                });

            await bot.SendTextMessageAsync(recipient.Id, $"\ud83d\udd14 Are you ready for coffee battle with @{sender.Username}? \ud83e\udd20", ParseMode.Default, false, false, 0, rkm);
            await bot.SendTextMessageAsync(sender.Id, $"Invitation for coffee battle has been sent to @{recipient.Username}! \ud83d\udc4c Wait for response please...");
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
                Console.WriteLine("Unknown sender! Add this one to database...");
                await userManager.CreateUserAsync(senderId, senderUsername, null, null);
            }

            return sender;
        }
    }
}