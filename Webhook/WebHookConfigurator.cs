using Common_Smart_House_bot_common.Configuration;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Webhook
{
    public class WebHookConfigurator(ITelegramBotClient botClient, IOptions<BotConfiguration> botConfiguration) : IHostedService
    {
        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            var webHookAddress = botConfiguration.Value.HostAddress + BotConfiguration.UpdateRoute;

            await botClient.SetWebhookAsync(
                url: webHookAddress,
                secretToken: botConfiguration.Value.SecretToken);
        }

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            await botClient.DeleteWebhookAsync();
        }
    }
}
