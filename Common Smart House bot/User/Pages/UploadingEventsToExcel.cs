using Common_Smart_House_bot.Services;
using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class UploadingEventsToExcel : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = "Загрузка событий в Excel выполнена.";

            var replyMarkup = GetReplyKeyboardMarkup();
            var path = "Resources\\Documents\\Export.xlsx";
            var resource = ResourcesService.GetResources(path);
            userState.AddPage(this);
            return new DocumentPageResult(resource, text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update!.CallbackQuery!.Data == "Назад")
            {
                userState.Pages.Pop();
                return userState.CurrentPage.View(update, userState);
            }
            if (update!.CallbackQuery!.Data == "На главную")
            {
                return new StartPage().View(update, userState);
            }
            return new PageResultBase("Выберете действие на кнопке", GetReplyKeyboardMarkup());
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
