using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot.User.Pages.PageResult
{
    public class DocumentPageResult : PageResultBase
    {
        public InputFile Document { get; set; }

        public DocumentPageResult(InputFile document, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Document = document;
        }
    }
}
