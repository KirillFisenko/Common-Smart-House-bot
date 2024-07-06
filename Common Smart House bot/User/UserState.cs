using Common_Smart_House_bot.User.Pages;

namespace Common_Smart_House_bot.User
{
    /// <summary>
    /// На какой странице и какие данные
    /// </summary>    
    public record class UserState(Stack<IPage> Pages, UserData UserData)
    {
        public IPage CurrentPage => Pages.Peek();
        public void AddPage(IPage page)
        {
            if (CurrentPage.GetType() != page.GetType())
            {
                Pages.Push(page);
            }
        }
    }
}
