using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class LastEvents : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Последние события

Дата	            Устройство	    Место	 Событие	Эко система
23.05.2024 13:08	Датчик климата	Спальня	 28’	    Aqara";

            var replyMarkup = GetReplyKeyboardMarkup();

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
            if (update.Message.Text == "Посмотреть больше")
            {
                return new AllEvents().View(update, userState);
            }
            if (update.Message.Text == "Скачать все события в Excel")
            {
                return new UploadingEventsToExcel().View(update, userState);
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
                    new KeyboardButton("Посмотреть больше")
                    ],
                    [
                    new KeyboardButton("Скачать все события в Excel")
                    ]
                 ])
            {
                ResizeKeyboard = true
            };
        }
    }
}