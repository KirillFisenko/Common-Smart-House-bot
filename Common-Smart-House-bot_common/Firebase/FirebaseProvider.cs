using Firebase.Database;
using Firebase.Database.Query;

namespace Common_Smart_House_bot_common.Firebase
{
    public class FirebaseProvider(FirebaseClient client)
    {
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
