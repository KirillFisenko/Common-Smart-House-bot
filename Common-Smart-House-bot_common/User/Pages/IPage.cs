using Common_Smart_House_bot_common.User.Pages.PageResult;
using Telegram.Bot.Types;

namespace Common_Smart_House_bot_common.User.Pages
{
    public interface IPage
    {
        PageResultBase View(Update update, UserState userState);

        PageResultBase Handle(Update update, UserState userState);
    }
}
