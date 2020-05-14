using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Core.Common;
using Core.Enums;
using Core.Interfaces;

namespace Core.Services
{
    public class CallbackHandler : ICallbackHandler
    {
        /// <summary>
        /// Process username and send message with invitation.
        /// </summary>
        /// <param name="callbackData">Telegram message.</param>
        /// <param name="client">Telegram client.</param>
        /// <param name="userManager">Manager of application users.</param>
        /// <returns>Operation result.</returns>
        public async Task<Result> Execute(string callbackData, ITelegramBotClient client, IUserManager userManager)
        {
            callbackData = callbackData ?? throw new ArgumentNullException(nameof(callbackData));

            InvitationResponce response;
            try
            {
                response = JsonConvert.DeserializeObject<InvitationResponce>(callbackData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Json deserialization ERROR: {ex}");
                return Result.Failure(new string[] { ex.Message });
            }

            Console.WriteLine($"Answer: {response.Answer} for User Id: {response.ToId}");

            var recipient = await userManager.GetUserByIdAsync(response.FromId);
            var sender = await userManager.GetUserByIdAsync(response.ToId);

            if (recipient == null || sender == null)
            {
                Console.WriteLine("Unknown recipient and sender...");
                return null;
            }

            switch (response.Answer)
            {
                case Answer.Accept:
                    {
                        Console.WriteLine("Sending ACCEPT messages for recipient and sender!");
                        await client.SendTextMessageAsync(recipient.Id, $"Your answer is been send to @{sender.Username}. Prepare for coffee battle!", ParseMode.Default, false, false, 0, replyMarkup: new ReplyKeyboardRemove());
                        await client.SendTextMessageAsync(sender.Id, $"@{recipient.Username} ACCEPT your invitation! Prepare for coffee battle!");
                    }
                    break;

                case Answer.Decline:
                    {
                        Console.WriteLine("Sending DECLINE messages for recipient and sender!");
                        await client.SendTextMessageAsync(recipient.Id, $"Your answer is been send to @{sender.Username}. Be readdy next time!", ParseMode.Default, false, false, 0, replyMarkup: new ReplyKeyboardRemove());
                        await client.SendTextMessageAsync(sender.Id, $"@{recipient.Username} DECLINE your invitation! Be readdy next time!");

                    }
                    break;
            }

            return Result.Success();
        }
    }
}