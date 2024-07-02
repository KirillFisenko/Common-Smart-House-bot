using Common_Smart_House_bot.Storage;
using Common_Smart_House_bot.User;
using Common_Smart_House_bot.User.Pages;
using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static UserStateStorage storage = new UserStateStorage();
    static async Task Main()
    {
        var TOKEN = "7267979726:AAEam6VLHENjtsPxqxwWTzorkntxerY3vBY";
        var telegramApiClient = new TelegramBotClient(TOKEN);
        var user = await telegramApiClient.GetMeAsync();
        Console.WriteLine($"{DateTime.Now} Начали слушать апдейты пользователя {user.Username}");
        telegramApiClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePoolingError);
        Console.ReadLine();
    }

    private static async Task HandlePoolingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine($"{DateTime.Now} {exception.Message}");
    }

    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message &&
            update.Type != Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            Console.WriteLine($"{DateTime.Now} update_id={update.Id} Не отправлено полезной информации");
            return;
        }

        long telegramUserId;
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            telegramUserId = update!.Message!.From!.Id;
        }
        else
        {
            telegramUserId = update!.CallbackQuery!.From.Id;
        }
        Console.WriteLine($"{DateTime.Now} update_id={update.Id} telegramuserId={telegramUserId}");

        var isExistUserState = storage.TryGet(telegramUserId, out var userState);

        if (!isExistUserState)
        {
            userState = new UserState(new NotStatedPage(), new UserData());
        }
        Console.WriteLine($"{DateTime.Now} update_id={update.Id} CURRENT_userState={userState}");

        var result = userState?.Page.Handle(update, userState);
        Console.WriteLine($"{DateTime.Now} update_id={update.Id} send_text={result.Text} UPDATED_UserState={result.UpdatedUserState}");
        try
        {
            switch (result)
            {
                case PhotoPageResult photoPageResult:
                    await client.SendPhotoAsync(
                                    chatId: telegramUserId,
                                    photo: photoPageResult.Photo,
                                    caption: result.Text,
                                    replyMarkup: result.ReplyMarkup,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                    );
                    break;
                case AudioPageResult audioPageResult:
                    await client.SendAudioAsync(
                                    chatId: telegramUserId,
                                    audio: audioPageResult.Audio,
                                    caption: result.Text,
                                    replyMarkup: result.ReplyMarkup,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                    );
                    break;
                case VideoPageResult videoPageResult:
                    await client.SendVideoAsync(
                                    chatId: telegramUserId,
                                    video: videoPageResult.Video,
                                    caption: result.Text,
                                    replyMarkup: result.ReplyMarkup,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                    );
                    break;
                case DocumentPageResult documentPageResult:
                    await client.SendDocumentAsync(
                                    chatId: telegramUserId,
                                    document: documentPageResult.Document,
                                    caption: result.Text,
                                    replyMarkup: result.ReplyMarkup,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                    );
                    break;
                default:
                    if (!isExistUserState)
                    {
                        await client.SendTextMessageAsync(
                                    chatId: telegramUserId,
                                    text: result.Text,
                                    replyMarkup: result.ReplyMarkup,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                    );
                    }
                    else
                    {
                        await client.EditMessageTextAsync(
                                    chatId: telegramUserId,
                                    messageId: update!.CallbackQuery!.Message!.MessageId,
                                    text: result.Text,
                                    replyMarkup: (InlineKeyboardMarkup)result.ReplyMarkup,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                    );
                    }
                    
                    break;
            }
        }
        catch
        {
            await client.SendTextMessageAsync(
                chatId: telegramUserId,
                text: "Возникла ошибка, попробуйте еще раз");
            Console.WriteLine($"{DateTime.Now} update_id={update.Id} {update.Message} error");
        }
        finally
        {
            storage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
        }
    }
}