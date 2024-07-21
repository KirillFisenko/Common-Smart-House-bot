using Common_Smart_House_bot.Firebase;
using Common_Smart_House_bot.User;
using Common_Smart_House_bot.User.Pages;

namespace Common_Smart_House_bot.Storage
{
    public class UserStateStorage
    {
        private readonly FirebaseProvider firebaseProvider = new();

        public async Task AddOrUpdateAsync(long telegramUserId, UserState userState)
        {
            var userStateFirebase = ToUserStateFirebase(userState);
            await firebaseProvider.AddOrUpdateAsync($"userStates/{telegramUserId}", userStateFirebase);
        }

        public async Task<UserState> TryGetAsync(long telegramUserId)
        {
            var userStateFirebase = await firebaseProvider.TryGetAsync<UserStateFirebase>($"userStates/{telegramUserId}");
            return userStateFirebase == null ? null : ToUserState(userStateFirebase);
        }

        private static UserStateFirebase ToUserStateFirebase(UserState userState)
        {
            return new UserStateFirebase
            {
                UserData = userState.UserData,
                PagesNames = userState.Pages.Select(x => x.GetType().Name).ToList()
            };
        }

        private static UserState ToUserState(UserStateFirebase userStateFirebase)
        {
            var pages = userStateFirebase.PagesNames.Select(PagesFactory.GetPage).Reverse();
            return new UserState(new Stack<IPage>(pages), userStateFirebase.UserData);
        }
    }
}
