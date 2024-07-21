using Firebase.Database;
using Firebase.Database.Query;

namespace Common_Smart_House_bot.Firebase
{
    public class FirebaseProvider
    {
        private const string BasePath = "https://common-smart-house-bot-default-rtdb.firebaseio.com/";
        private const string Secret = "IPQf8F887AUMm2eHcEbig11BV6ypcoAQhqRCxMHW";

        private readonly FirebaseClient client;

        public FirebaseProvider()
        {
            client = new FirebaseClient(BasePath, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(Secret)
            });
        }

        public async Task<T> TryGetAsync<T>(string key)
        {
            return await client.Child(key).OnceSingleAsync<T>();
        }

        public async Task AddOrUpdateAsync<T>(string key, T item)
        {
            await client.Child(key).PutAsync(item);
        }
    }
}
