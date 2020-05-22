using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Telegram.Bot.Types;
using Core.Resources;

namespace Core.Services
{
    /// <summary>
    /// Class to manage user's callback.
    /// </summary>
    public class CallbackHandler : ICallbackHandler
    {
        /// <inheritdoc/>
        public async Task<Result> Execute(CallbackQuery callbackQuery, ITelegramBotClient client, IUserManager userManager)
        {
            callbackQuery = callbackQuery ?? throw new ArgumentNullException(nameof(callbackQuery));

            var callbackData = callbackQuery.Data;
            var message = callbackQuery.Message;

            InvitationResponce response;
            try
            {
                response = JsonConvert.DeserializeObject<InvitationResponce>(callbackData);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { ex.Message });
            }

            Console.WriteLine($"Answer: {response.Answer} for User Id: {response.ToId}");

            var recipient = await userManager.GetUserByIdAsync(response.FromId);
            var sender = await userManager.GetUserByIdAsync(response.ToId);

            if (recipient == null || sender == null)
            {
                return null;
            }

            switch (response.Answer)
            {
                case Answer.Accept:
                    {
                        var recipientText = string.Format(Callback.AnswerSent, sender.Username);
                        var senderText = string.Format(Callback.AcceptInvitation, recipient.Username);

                        await client.SendTextMessageAsync(recipient.Id, $"{recipientText} {Callback.Prepare} \ud83d\udcaa", ParseMode.Default, false, false, 0, replyMarkup: new ReplyKeyboardRemove());
                        await client.SendTextMessageAsync(sender.Id, $"{senderText} \ud83e\udd1d \ud83d\ude0e {Callback.Prepare} \ud83d\udcaa");
                    }
                    break;

                case Answer.Decline:
                    {
                        var recipientText = string.Format(Callback.AnswerSent, sender.Username);
                        var senderText = string.Format(Callback.DeclineInvitation, recipient.Username);

                        await client.SendTextMessageAsync(recipient.Id, $"{recipientText} {Callback.GetReady} \ud83d\udc4c", ParseMode.Default, false, false, 0, replyMarkup: new ReplyKeyboardRemove());
                        await client.SendTextMessageAsync(sender.Id, $"{senderText} \ud83e\udd2d \ud83d\ude2d {Callback.GetReady} \ud83d\udc4c");
                    }
                    break;
            }

            await client.EditMessageReplyMarkupAsync(message.Chat.Id, message.MessageId, replyMarkup: null);
            return Result.Success();
        }
    }
}