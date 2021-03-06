﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Interfaces;
using Core.Resources;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Core.Commands
{
    /// <summary>
    /// Command to process username and send message with invitation.
    /// </summary>
    public class InviteByRandomCommand : InviteCommand, ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = CommandConstants.RandomKey;

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
            var users = await userManager.GetUsersAsync();
            if (users.Count <= 2)
            {
                var errorMessage = string.Format(InviteByRandom.Message, users.Count);
                await client.SendTextMessageAsync(sender.Id, errorMessage);
                return;
            }

            // Remove sender from users list.
            users = users.Where(x => x.Id != sender.Id).ToArray();

            // Select random recipient.
            Random rand = new Random();
            var index = rand.Next(users.Count);
            var recipient = users.ElementAt(index);

            // Send invitation.
            await Invite(sender, recipient, client);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}