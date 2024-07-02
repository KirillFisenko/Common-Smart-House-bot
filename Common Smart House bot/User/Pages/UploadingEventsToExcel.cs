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

            return new DocumentPageResult(resource, text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.Message.Text == "Назад")
            {
                return new LastEvents().View(update, userState);
            }
            if (update.Message.Text == "На главную")
            {
                return new StartPage().View(update, userState);
            }
            return new PageResultBase("Выберете действие на кнопке", GetReplyKeyboardMarkup());
        }

        private ReplyKeyboardMarkup GetReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                    new KeyboardButton("Назад")
                    ],
                    [
                    new KeyboardButton("На главную")
                    ]
                 ])
            {
                ResizeKeyboard = true
            };
        }
    }
}
