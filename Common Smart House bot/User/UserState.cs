using Common_Smart_House_bot.User.Pages;

namespace Common_Smart_House_bot.User
{
    /// <summary>
    /// На какой странице и какие данные
    /// </summary>    
    public record class UserState(IPage Page, UserData UserData);
}
