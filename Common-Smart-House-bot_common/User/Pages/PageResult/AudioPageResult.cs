using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot_common.User.Pages.PageResult
{
    public class AudioPageResult : PageResultBase
    {
        public InputFile Audio { get; set; }

        public AudioPageResult(InputFile audio, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Audio = audio;
        }
    }
}