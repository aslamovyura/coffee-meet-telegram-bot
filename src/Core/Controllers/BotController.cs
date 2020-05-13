using Core.Interfaces;
using Core.Models;
//using Core.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Core.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;
        private readonly IUserManager _userManager;

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="commandService">Interface to use the command service.</param>
        /// <param name="telegramBotClient">Interface to use the Telegram Bot API.</param>
        /// <param name="userManager">Manager of application users.</param>
        public BotController(ICommandService commandService,
                             ITelegramBotClient telegramBotClient,
                             IUserManager userManager)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Request processing method.
        /// </summary>
        /// <param name="update">Incoming update.</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update == null)
            {
                return NoContent();
            }

            var message = update.Message;

            Console.WriteLine(string.Format("---> Message was recieved {0}, text:{1}  User Id:{2}, message type:{3}", message.Chat.Username, message.Text, message.From.Id, message.Type));

            foreach (var command in _commandService.Get())
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, _telegramBotClient, _userManager);
                    break;
                }
            }

            return Ok();
        }
    }
}