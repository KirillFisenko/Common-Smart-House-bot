using Common_Smart_House_bot.Storage;
using Telegram.Bot;
using Telegram.Bot.Polling;

internal partial class Program
{
    private static UserStateStorage storage = new UserStateStorage();

    private static async Task Main()
    {
        var TOKEN = "7267979726:AAEam6VLHENjtsPxqxwWTzorkntxerY3vBY";
        var telegramApiClient = new TelegramBotClient(TOKEN);
        var user = await telegramApiClient.GetMeAsync();
        Console.WriteLine($"{DateTime.Now} Начали слушать апдейты пользователя {user.Username}");
        telegramApiClient.StartReceiving(IUpdateHandler updateHandler);
        Console.ReadLine();
    }
}