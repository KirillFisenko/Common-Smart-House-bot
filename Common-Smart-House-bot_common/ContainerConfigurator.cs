using Common_Smart_House_bot_common.Configuration;
using Common_Smart_House_bot_common.Firebase;
using Common_Smart_House_bot_common.Storage;
using Firebase.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Common_Smart_House_bot_common
{
    public class ContainerConfigurator
    {
        public static void Configure(IConfiguration configuration, IServiceCollection services)
        {
            var fireBaseConfigurationSection = configuration.GetSection(FireBaseConfiguration.SectionName);
            services.Configure<FireBaseConfiguration>(fireBaseConfigurationSection);

            var botConfigurationSection = configuration.GetSection(BotConfiguration.SectionName);
            services.Configure<BotConfiguration>(botConfigurationSection);

            services.AddSingleton<UserStateStorage>();
            services.AddSingleton<FirebaseProvider>();
            services.AddSingleton<FirebaseClient>(services =>
            {
                var fireBaseConfig = services.GetService<IOptions<FireBaseConfiguration>>()!.Value;
                return new FirebaseClient(fireBaseConfig.BasePath, new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(fireBaseConfig.Secret)
                });
            });

            services.AddHttpClient("tg_bot_client").AddTypedClient<ITelegramBotClient>((httpClient, services) =>
            {
                var botConfig = services.GetService<IOptions<BotConfiguration>>()!.Value;
                var options = new TelegramBotClientOptions(botConfig.BotToken);
                return new TelegramBotClient(options, httpClient);
            });

            services.AddSingleton<IUpdateHandler, UpdateHandler>();
        }
    }
}
