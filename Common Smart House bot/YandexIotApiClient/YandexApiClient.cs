using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Common_Smart_House_bot.YandexIotApiClient
{
    public class YandexApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string token = "y0_AgAAAAACAv5iAAwfPwAAAAEKzZK6AAAyf6w4Y0NAcZYoArn0aIojy4TKuA";

        static YandexApiClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> SendDeviceActionAsync<T>(T requestBody)
        {
            var url = "https://api.iot.yandex.net/v1.0/devices/actions";
            var response = await client.PostAsJsonAsync(url, requestBody);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task TurnLightOnOff(string device_id, bool action)
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

            var response = await new YandexApiClient().SendDeviceActionAsync(requestBody);
            Console.WriteLine(response);
        }
    }
}