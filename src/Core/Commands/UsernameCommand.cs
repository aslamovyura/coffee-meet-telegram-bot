using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Requests;

namespace Core.Commands
{
    public class UsernameCommand : ITelegramCommand
    {
        public string Name { get; } = "@";

        public async Task Execute(Message message, ITelegramBotClient client)
        {
            Console.WriteLine("Execution of username command!");

            // Sender parameters.
            var chatId = message.Chat.Id;
            var senderUsername = message.From.Username;


            //// Recipient parameters.
            //var user = message.From;
            //var administrators = await client.GetChatAdministratorsAsync(chatId);

            //foreach (var adm in administrators)
            //{
            //    Console.WriteLine($"Admin: {adm.User.Username}, ID: {adm.User.Id}, Bot: {adm.User.IsBot}");
            //}

            var botId = client.BotId;
            var bot = await client.GetChatMemberAsync(chatId, botId);

            Console.WriteLine($"Bot id = {botId}");
        }

        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}