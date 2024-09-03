using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot_common.User.Pages.PageResult
{
    public class PhotoPageResult : PageResultBase
    {
        public InputFile Photo { get; set; }

        public PhotoPageResult(InputFile photo, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Photo = photo;
        }
    }
}
