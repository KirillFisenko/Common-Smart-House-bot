using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class AllEvents : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Все события

Дата	                Устройство	     Место	    Событие	    Эко система
23.05.2024 13:08	    Датчик климата	 Спальня	28’	        Aqara
21.05.2024 14:08	    Свет	         Зал	    Включение	Яндекс
20.05.2024 11:08	    Умная кнопка	 Кухня	    Нажатие	    Aqara";

            var replyMarkup = GetReplyKeyboardMarkup();
            userState.AddPage(this);
            return new PageResultBase(text, replyMarkup)
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
            if (update!.CallbackQuery!.Data == "Скачать все события в Excel")
            {
                return new UploadingEventsToExcel().View(update, userState);
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
                    InlineKeyboardButton.WithCallbackData("Скачать все события в Excel")
                    ]
                    ]);
        }
    }
}
