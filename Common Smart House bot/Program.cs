using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
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
        if (update.Message?.Text != null)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;
            var messageId = update.Message.MessageId;
            var errorText = "Возникла ошибка, попробуйте еще раз";

            try
            {
                // Отправить обычные кнопки
                if (update.Message.Text.StartsWith("/button"))
                {
                    var n = int.Parse(text.Split()[1]);
                    var m = int.Parse(text.Split()[2]);
                    var keyboard = GetReplyKeyboard(n, m);

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Вы прислали \n {text}",
                        replyMarkup: new ReplyKeyboardMarkup(keyboard)
                        {
                            ResizeKeyboard = true
                        });
                }

                // Отправить инлайн кнопки
                else if (update.Message.Text.StartsWith("/inline_buttons"))
                {
                    var n = int.Parse(text.Split()[1]);
                    var m = int.Parse(text.Split()[2]);
                    var keyboard = GetInlineKeyboard(n, m);

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Вы прислали \n {text}",
                        replyMarkup: new InlineKeyboardMarkup(keyboard));
                }

                // Отправить рандомно фото из библиотеки
                else if (update.Message.Text == "/photo")
                {
                    var imagePaths = new string[]
                    {
                        "1.jpg",
                        "2.jpg",
                        "3.jpg",
                        "4.jpg"
                    };

                    var randomPhotoIndex = new Random().Next(imagePaths.Length);

                    using (var fileStream = new FileStream($@"Images\{imagePaths[randomPhotoIndex]}", FileMode.Open))
                    {
                        await client.SendPhotoAsync(
                            chatId: chatId,
                            photo: InputFile.FromStream(fileStream));
                    }
                }

                // Отправить фото по ссылке
                else if (update.Message.Text.StartsWith("/photo") && text.Split().Length == 2)
                {
                    var url = text.Split()[1];
                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: InputFile.FromString(url));
                }

                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Такой команды не существует");
                }
            }
            catch
            {
                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: errorText);
            }
        }
    }

    /// <summary>
    /// Получить матрицу n*m обычных кнопок
    /// </summary>    
    private static List<List<KeyboardButton>> GetReplyKeyboard(int n, int m)
    {
        var keyboard = new List<List<KeyboardButton>>();
        var index = 1;
        for (var i = 0; i < n; i++)
        {
            var row = new List<KeyboardButton>();
            for (var j = 0; j < m; j++)
            {
                row.Add(new KeyboardButton(index.ToString()));
                index++;
            }
            keyboard.Add(row);
        }
        return keyboard;
    }

    /// <summary>
    /// Получить матрицу n*m инлайн кнопок
    /// </summary>   
    private static List<List<InlineKeyboardButton>> GetInlineKeyboard(int n, int m)
    {
        var keyboard = new List<List<InlineKeyboardButton>>();
        var index = 1;
        for (var i = 0; i < n; i++)
        {
            var row = new List<InlineKeyboardButton>();
            for (var j = 0; j < m; j++)
            {
                row.Add(InlineKeyboardButton.WithCallbackData(index.ToString()));
                index++;
            }
            keyboard.Add(row);
        }
        return keyboard;
    }
}