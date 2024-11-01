using Telegram.Bot.Types.ReplyMarkups;

namespace Common_Smart_House_bot_common.User.Pages.PageResult
{
    public class PageResultBase
    {
        public string Text { get; }
        public IReplyMarkup ReplyMarkup { get; }
        public UserState UpdatedUserState { get; set; }

        public PageResultBase(string text, IReplyMarkup replyMarkup)
        {
            Text = text;
            ReplyMarkup = replyMarkup;
        }

        public bool IsMedia => this is PhotoPageResult;
    }
}