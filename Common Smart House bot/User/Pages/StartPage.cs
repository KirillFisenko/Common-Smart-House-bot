using Common_Smart_House_bot.User.Pages.PageResult;
using Common_Smart_House_bot_common.Services;
using Common_Smart_House_bot_common.User.Pages;
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
            var image = Resources.StartPage;
            var resource = ResourcesService.GetResources(image);
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
