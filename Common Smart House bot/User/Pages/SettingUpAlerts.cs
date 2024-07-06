using Common_Smart_House_bot.Services;
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
            var path = "Resources\\Audios\\notification.mp3";
            var resource = ResourcesService.GetResources(path);
            userState.AddPage(this);
            return new AudioPageResult(resource, text, replyMarkup)
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
            return new PageResultBase("Выберете действие на кнопке", GetReplyKeyboardMarkup());
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
