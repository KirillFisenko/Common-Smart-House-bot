using Common_Smart_House_bot_common.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot_common.User.Pages
{
    public class SettingUpAlertsPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Текущие оповещения

	Устройство	    Место	   Событие	   Эко система
	Датчик климата	Спальня	   28’	       Aqara
	Свет	        Зал	       Включение   Яндекс
	Умная кнопка	Кухня	   Нажатие	   Aqara";

            var replyMarkup = GetReplyKeyboardMarkup();
            userState.AddPage(this);
            return new PageResultBase(text, replyMarkup)
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
            return View(update, userState);
        }

        private IReplyMarkup GetReplyKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                    InlineKeyboardButton.WithCallbackData("Назад")
                    ]
                    ]);
        }
    }
}
