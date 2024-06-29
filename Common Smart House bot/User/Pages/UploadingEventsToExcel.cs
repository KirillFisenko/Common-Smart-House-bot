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
            var documentUrl = "https://drive.google.com/uc?export=download&id=16ssE7zCqwessPRro3VZ5wQkztgxUHuoc";
            
            return new PageResultBase(text, replyMarkup)
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
