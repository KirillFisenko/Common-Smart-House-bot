using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class LastEventsPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Последние события

Дата	            Устройство	    Место	 Событие	Эко система
23.05.2024 13:08	Датчик климата	Спальня	 28’	    Aqara";

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
            if (update!.CallbackQuery!.Data == "Посмотреть больше")
            {
                return new AllEventsPage().View(update, userState);
            }
            if (update!.CallbackQuery!.Data == "Скачать все события в Excel")
            {
                return new UploadingEventsToExcelPage().View(update, userState);
            }
            return View(update, userState);
        }

        private IReplyMarkup GetReplyKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                    InlineKeyboardButton.WithCallbackData("Назад")
                    ],
                    [
                    InlineKeyboardButton.WithCallbackData("Посмотреть больше")
                    ],
                    [
                    InlineKeyboardButton.WithCallbackData("Скачать все события в Excel")
                    ]
                 ]);
        }
    }
}