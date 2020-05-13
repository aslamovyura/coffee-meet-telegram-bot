using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    public class ContactCommand : ITelegramCommand
    {

        public string Name { get; } = "";

        public async Task Execute(Message message, ITelegramBotClient client)
        {
            // Sender parameters.
            var senderId = message.Chat.Id;
            var senderUsername = message.Chat.Username;

            // Recipient parameters.
            var userId = message.Contact.UserId;
            var firstName = message.Contact.FirstName;
            var lastName = message.Contact.LastName;

            Console.WriteLine($"Trying to find chat member {firstName} {lastName} with ID: {userId}...");
            var recipient = await client.GetChatMemberAsync(userId, userId);

            if (recipient == null)
            {
                Console.WriteLine("Chat member not found!");
                await client.SendTextMessageAsync(senderId, "User is not a member of CoffeeBot! Try again!");
                return;
            }

            //var chat = await client.GetChatAsync(chatId);
            Console.WriteLine($"Send invitation to battle to {firstName} {lastName}, ID:{userId}?");
            await client.SendTextMessageAsync(userId, $"Are you ready for coffee battle with {senderUsername}?");
        }

        public bool Contains(Message message) => message.Type != MessageType.Contact ? false : true;
    }
}