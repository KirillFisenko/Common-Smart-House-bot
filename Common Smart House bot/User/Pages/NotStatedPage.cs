using Common_Smart_House_bot.User.Pages.PageResult;
using Telegram.Bot.Types;

namespace Common_Smart_House_bot.User.Pages
{
    public class NotStatedPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            return null!;
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            return new StartPage().View(update, userState);
        }
    }
}
