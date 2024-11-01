using Common_Smart_House_bot;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Common_Smart_House_bot_common.YandexIotApiClient
{
    public class YandexApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        static YandexApiClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.YandexToken);
        }

        public async Task<string> SendDeviceActionAsync<T>(T requestBody, string url)
        {
            var response = await client.PostAsJsonAsync(url, requestBody);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetUserInfoAsync()
        {
            var url = "https://api.iot.yandex.net/v1.0/user/info";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task TurnOnOff(string device_id, bool action)
        {
            var requestBody = new
            {
                devices = new[]
                {
                    new
                    {
                        id = device_id,
                        actions = new[]
                        {
                            new
                            {
                                type = "devices.capabilities.on_off",
                                state = new
                                {
                                    instance = "on",
                                    value = action
                                }
                            }
                        }
                    }
                }
            };
            var url = "https://api.iot.yandex.net/v1.0/devices/actions";
            var response = await new YandexApiClient().SendDeviceActionAsync(requestBody, url);
            Console.WriteLine(response);
        }
    }
}