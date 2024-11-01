﻿using Firebase.Database;
using Firebase.Database.Query;

namespace Common_Smart_House_bot.Firebase
{
    public class FirebaseProvider
    {
        private readonly FirebaseClient client;

        public FirebaseProvider()
        {
            client = new FirebaseClient(Configuration.FireBaseBasePath, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(Configuration.FireBaseSecret)
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
