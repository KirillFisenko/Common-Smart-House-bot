using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class SettingUpAlerts : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Текущие оповещения

	Устройство	    Место	   Событие	   Эко система
	Датчик климата	Спальня	   28’	       Aqara
	Свет	        Зал	       Включение   Яндекс
	Умная кнопка	Кухня	   Нажатие	   Aqara";

            var replyMarkup = GetReplyKeyboardMarkup();
            var audioUrl = "https://drive.google.com/uc?export=download&id=1sxJbvnS7hdKDJ6lNgnpHjpdRZ1E_QSXH";
            
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
