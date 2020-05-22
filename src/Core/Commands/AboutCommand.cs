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
    /// About command for telegram bot.
    /// </summary>
    public class AboutCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Key { get; } = CommandConstants.AboutKey;

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client, IUserManager userManager)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, $"{About.OpenSounceProject} \ud83d\udcc2 {About.MoreDetails} \ud83d\udc49 {About.Link} \ud83d\udc48 ");
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Key);
    }
}