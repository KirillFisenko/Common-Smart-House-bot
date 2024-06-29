using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages
{
    public class StartPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Добро пожаловать в Common Smart Home - твой помощник умного дома.
Поддерживаемые системы: Яндекс, Xiaomi, Aqara, LG, IFEEL, Polaris, Roborock, VIDAA, Samsung.";

            var replyMarkup = GetReplyKeyboardMarkup();
            var photoUrl = "https://drive.google.com/uc?export=download&id=1fy5bFlKGosTZ3-NfVbLnD3_BLAwRycyG";
        
            return new PhotoPageResult(InputFile.FromUri(photoUrl), text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {            
            if (update.Message.Text == "Управление умным домом")
            {
                return new SmartHomeManagement().View(update, userState);
            }
            if (update.Message.Text == "Просмотр всех событий")
            {
                return new LastEvents().View(update, userState);
            }
            if (update.Message.Text == "Настройка оповещений")
            {
                return new SettingUpAlerts().View(update, userState);
            }
            return new PageResultBase("Выберете действие на кнопке", GetReplyKeyboardMarkup()); ;
        }

        private ReplyMarkupBase GetReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                    new KeyboardButton("Управление умным домом")
                    ],
                    [
                    new KeyboardButton("Просмотр всех событий")
                    ],
                    [
                    new KeyboardButton("Настройка оповещений")
                    ]
                    ])
            {
                ResizeKeyboard = true
            };
        }
    }
}
