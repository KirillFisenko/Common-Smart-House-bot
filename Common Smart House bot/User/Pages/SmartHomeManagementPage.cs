using Common_Smart_House_bot.User.Pages.PageResult;
using Common_Smart_House_bot.YandexIotApiClient;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static Common_Smart_House_bot.YandexIotApiClient.UserInfoRequest;

namespace Common_Smart_House_bot.User.Pages
{
    public class SmartHomeManagementPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Управление умным домом";
            var rootObject = UserInfoRequest.GetYandexUserInfo();
            var devicesTypes = GetYandexDevicesTypes(rootObject);
            var yandexRooms = GetYandexRooms(rootObject);
            var replyMarkup = GetReplyKeyboardMarkup(rootObject.devices, yandexRooms);
            userState.AddPage(this);
            return new PageResultBase(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.CallbackQuery == null)
            {
                return View(update, userState);
            }

            var rootObject = UserInfoRequest.GetYandexUserInfo();
            var devicesTypes = GetYandexDevicesTypes(rootObject);
            var yandexRooms = GetYandexRooms(rootObject);
            var lightDevices = rootObject.devices.Where(device => device.type == "devices.types.light");
            var socketDevices = rootObject.devices.Where(device => device.type == "devices.types.socket");

            foreach (var device in lightDevices)
            {
                var roomName = yandexRooms.FirstOrDefault(room => room.id == device.room).name;

                if (update.CallbackQuery.Data == $"Вкл. {device.name} {roomName}")
                {
                    YandexApiClient.TurnLightOnOff(device.id, true);
                    return View(update, userState);
                }

                if (update.CallbackQuery.Data == $"Выкл. {device.name} {roomName}")
                {
                    YandexApiClient.TurnLightOnOff(device.id, false);
                    return View(update, userState);
                }
            }

            if (update!.CallbackQuery!.Data == "Назад")
            {
                userState.Pages.Pop();
                return userState.CurrentPage.View(update, userState);
            }
            return View(update, userState);
        }

        private IReplyMarkup GetReplyKeyboardMarkup(IEnumerable<Device> devices, IEnumerable<Room> rooms)
        {
            var keyboard = new List<List<InlineKeyboardButton>>();
            var lightDevices = devices.Where(device => device.type == "devices.types.light");
            var socketDevices = devices.Where(device => device.type == "devices.types.socket");

            foreach (var device in lightDevices)
            {
                var roomName = rooms.FirstOrDefault(room => room.id == device.room).name;
                var row = new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData($"Вкл. {device.name} {roomName}"),
                    InlineKeyboardButton.WithCallbackData($"Выкл. {device.name} {roomName}")
                };
                keyboard.Add(row);
            }

            foreach (var device in socketDevices)
            {
                var roomName = rooms.FirstOrDefault(room => room.id == device.room).name;
                var row = new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData($"Вкл. {device.name} {roomName}"),
                    InlineKeyboardButton.WithCallbackData($"Выкл. {device.name} {roomName}")
                };
                keyboard.Add(row);
            }

            var lastRow = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Назад")
            };
            keyboard.Add(lastRow);

            return new InlineKeyboardMarkup(keyboard);
        }

        private IEnumerable<Room> GetYandexRooms(Rootobject rootObject)
        {
            return rootObject.rooms;
        }

        private IEnumerable<string> GetYandexDevicesTypes(Rootobject rootObject)
        {
            return rootObject.devices.Select(device => device.type).Distinct();
        }
    }
}