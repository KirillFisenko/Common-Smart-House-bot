using Common_Smart_House_bot_common.Storage;
using Common_Smart_House_bot_common.User;
using Common_Smart_House_bot_common.User.Pages;
using Common_Smart_House_bot_common.User.Pages.PageResult;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot_common
{
    public class UpdateHandler(UserStateStorage storage) : IUpdateHandler
    {
        public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine($"{DateTime.Now} {exception.Message}");
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message &&
                update.Type != Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                Console.WriteLine($"{DateTime.Now} update_id={update.Id} Не отправлено полезной информации");
                return;
            }

            long telegramUserId = update.Type == Telegram.Bot.Types.Enums.UpdateType.Message ? update!.Message!.From!.Id : update!.CallbackQuery!.From.Id;
            Console.WriteLine($"{DateTime.Now} update_id={update.Id} telegramUserId={telegramUserId}");

            var userState = await storage.TryGetAsync(telegramUserId);

            userState ??= new UserState(new Stack<IPage>([new NotStatedPage()]), new UserData());
            Console.WriteLine($"{DateTime.Now} update_id={update.Id} CURRENT_userState={userState}");

            var result = userState!.CurrentPage.Handle(update, userState);
            Console.WriteLine($"{DateTime.Now} update_id={update?.Id} send_text={result?.Text} UPDATED_UserState={result?.UpdatedUserState}");
            var lastMessage = await SendResult(client, update, telegramUserId, result);
            if (result != null)
            {
                result.UpdatedUserState.UserData.LastMessage = new User.Message(lastMessage.MessageId, result.IsMedia);
                storage.AddOrUpdateAsync(telegramUserId, result.UpdatedUserState);
            }
        }

        private static async Task<Telegram.Bot.Types.Message?> SendResult(ITelegramBotClient client, Update update, long telegramUserId, PageResultBase? result)
        {
            switch (result)
            {
                case null:
                    return null;
                case PhotoPageResult photoPageResult:
                    return await SendPhoto(client, update, telegramUserId, photoPageResult);
                default:
                    return await SendText(client, update, telegramUserId, result);
            }
        }

        private static async Task<Telegram.Bot.Types.Message> SendPhoto(ITelegramBotClient client, Update update, long telegramUserId, PhotoPageResult result)
        {
            if (update.CallbackQuery != null && (result.UpdatedUserState.UserData.LastMessage?.IsMedia ?? false))
            {
                return await client.EditMessageMediaAsync(
                        chatId: telegramUserId,
                        messageId: result.UpdatedUserState.UserData.LastMessage.Id,
                        media: new InputMediaPhoto(result.Photo)
                        {
                            Caption = result.Text,
                            ParseMode = Telegram.Bot.Types.Enums.ParseMode.Html
                        },
                        replyMarkup: (InlineKeyboardMarkup)result.ReplyMarkup
                        );
            }

            if (result.UpdatedUserState.UserData.LastMessage != null)
            {
                await client.DeleteMessageAsync(chatId: telegramUserId,
                                                messageId: result.UpdatedUserState.UserData.LastMessage.Id);
            }

            return await client.SendPhotoAsync(
                                            chatId: telegramUserId,
                                            photo: result.Photo,
                                            caption: result.Text,
                                            replyMarkup: result.ReplyMarkup,
                                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                            );
        }

        private static async Task<Telegram.Bot.Types.Message> SendText(ITelegramBotClient client, Update update, long telegramUserId, PageResultBase? result)
        {
            if (update.CallbackQuery != null && (!result.UpdatedUserState.UserData.LastMessage?.IsMedia ?? false))
            {
                return await client.EditMessageTextAsync(
                        chatId: telegramUserId,
                        messageId: result.UpdatedUserState.UserData.LastMessage!.Id,
                        text: result.Text,
                        replyMarkup: (InlineKeyboardMarkup)result.ReplyMarkup,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                        );
            }

            if (result.UpdatedUserState.UserData.LastMessage != null)
            {
                await client.DeleteMessageAsync(chatId: telegramUserId,
                                                messageId: result.UpdatedUserState.UserData.LastMessage.Id);
            }

            return await client.SendTextMessageAsync(
                            chatId: telegramUserId,
                            text: result.Text,
                            replyMarkup: result.ReplyMarkup,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                            );
        }
    }
}