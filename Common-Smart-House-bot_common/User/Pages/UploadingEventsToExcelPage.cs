using Common_Smart_House_bot;
using Common_Smart_House_bot_common.Services;
using Common_Smart_House_bot_common.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot_common.User.Pages
{
    public class UploadingEventsToExcelPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = "Загрузка событий в Excel выполнена.";

            var replyMarkup = GetReplyKeyboardMarkup();
            var file = Resources.Export;
            var resource = ResourcesService.GetResources(file);
            userState.AddPage(this);
            return new DocumentPageResult(resource, text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.CallbackQuery == null)
            {
                return View(update, userState);
            }
            if (update!.CallbackQuery!.Data == "Назад")
            {
                userState.Pages.Pop();
                return userState.CurrentPage.View(update, userState);
            }
            return update!.CallbackQuery!.Data == "На главную" ? new StartPage().View(update, userState) : View(update, userState);
        }

        private IReplyMarkup GetReplyKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                    InlineKeyboardButton.WithCallbackData("Назад")
                    ],
                    [
                    InlineKeyboardButton.WithCallbackData("На главную")
                    ]
                 ]);
        }
    }
}
