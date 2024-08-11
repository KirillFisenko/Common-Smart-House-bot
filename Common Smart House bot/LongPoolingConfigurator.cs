using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Common_Smart_House_bot
{
    public class LongPoolingConfigurator(ITelegramBotClient botClient, IUpdateHandler updateHandler) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var user = await botClient.GetMeAsync();
            Console.WriteLine($"Начали слушать апдейты {user.Username}");
            await botClient.ReceiveAsync(updateHandler: updateHandler);
        }
    }
}
