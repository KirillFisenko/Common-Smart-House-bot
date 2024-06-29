using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class SmartHomeManagement : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Управление умным домом
Дальнейший функционал не реализован, вернитесь назад";

            var replyMarkup = GetReplyKeyboardMarkup();
            var videoUrl = "https://drive.google.com/uc?export=download&id=123QDAvjDALWe1hQKojL_QxYW7qbSDS8v";

            return new PageResultBase(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {           
            if (update.Message.Text == "Назад")
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
                    ]
                    ])
            {
                ResizeKeyboard = true
            };
        }
    }
}