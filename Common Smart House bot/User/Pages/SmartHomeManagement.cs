using Common_Smart_House_bot.Services;
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
            var path = "Resources\\Videos\\invideo.mp4";
            var resource = ResourcesService.GetResources(path);

            return new PageResultBase(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update!.CallbackQuery!.Data == "Назад")
            {
                return new StartPage().View(update, userState);
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