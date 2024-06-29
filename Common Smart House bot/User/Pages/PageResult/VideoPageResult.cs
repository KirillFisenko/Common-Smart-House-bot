using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages.PageResult
{
    public class VideoPageResult : PageResultBase
    {
        public InputFile Video { get; set; }

        public VideoPageResult(InputFile video, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Video = video;
        }
    }
}
