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
            var text = Resources.StartPageText;
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
            return update.CallbackQuery == null
                ? View(update, userState)
                : update.CallbackQuery.Data == "Управление умным домом"
                ? new SmartHomeManagementPage().View(update, userState)
                : View(update, userState);
        }

        private IReplyMarkup GetReplyKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                    InlineKeyboardButton.WithCallbackData("Управление умным домом")
                    ]
                 ]);
        }
    }
}
