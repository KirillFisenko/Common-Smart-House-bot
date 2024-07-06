using Common_Smart_House_bot.Services;
using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class StartPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Добро пожаловать в <b>Common Smart Home</b> - твой помощник умного дома.
Поддерживаемые системы: Яндекс, Xiaomi, Aqara, LG, IFEEL, Polaris, Roborock, VIDAA, Samsung.";

            var replyMarkup = GetReplyKeyboardMarkup();
            var path = "Resources\\Images\\StartPage.jpg";
            var resource = ResourcesService.GetResources(path);
            userState.AddPage(this);
            return new PhotoPageResult(resource, text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update!.CallbackQuery!.Data == "Управление умным домом")
            {
                return new SmartHomeManagement().View(update, userState);
            }
            if (update!.CallbackQuery!.Data == "Просмотр всех событий")
            {
                return new LastEvents().View(update, userState);
            }
            if (update!.CallbackQuery!.Data == "Настройка оповещений")
            {
                return new SettingUpAlerts().View(update, userState);
            }
            return new PageResultBase("Выберете действие на кнопке", GetReplyKeyboardMarkup()); ;
        }

        private IReplyMarkup GetReplyKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                    InlineKeyboardButton.WithCallbackData("Управление умным домом")
                    ],
                    [
                    InlineKeyboardButton.WithCallbackData("Просмотр всех событий")
                    ],
                    [
                    InlineKeyboardButton.WithCallbackData("Настройка оповещений")
                    ]
                 ]);
        }
    }
}
