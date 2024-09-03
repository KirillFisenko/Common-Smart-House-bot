using Common_Smart_House_bot_common.User.Pages.PageResult;
using Common_Smart_House_bot_common.YandexIotApiClient;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static Common_Smart_House_bot_common.YandexIotApiClient.UserInfoRequest;

namespace Common_Smart_House_bot_common.User.Pages
{
    public class SmartHomeManagementPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Управление умным домом";
            var rootObject = GetYandexUserInfo();
            var yandexRooms = rootObject.rooms;
            var replyMarkup = GetReplyKeyboardMarkup(rootObject, yandexRooms);
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

            var rootObject = GetYandexUserInfo();

            foreach (var group in rootObject.groups)
            {

                if (update.CallbackQuery.Data == $"Вкл. {group.name}")
                {
                    try
                    {
                        foreach (var device in group.devices)
                        {
                            YandexApiClient.TurnOnOff(device, true);
                        }
                    }
                    catch { }
                    return null;
                }

                if (update.CallbackQuery.Data == $"Выкл. {group.name}")
                {
                    try
                    {
                        foreach (var device in group.devices)
                        {
                            YandexApiClient.TurnOnOff(device, false);
                        }
                    }
                    catch { }
                    return null;
                }
            }

            //foreach (var device in rootObject.devices)
            //{
            //    var roomName = rootObject.rooms.FirstOrDefault(room => room.id == device.room).name;

            //    if (update.CallbackQuery.Data == $"Вкл. {device.name} {roomName}")
            //    {
            //        try
            //        {
            //            YandexApiClient.TurnOnOff(device.id, true);
            //        }
            //        catch { }
            //        return null;
            //    }

            //    if (update.CallbackQuery.Data == $"Выкл. {device.name} {roomName}")
            //    {
            //        try
            //        {
            //            YandexApiClient.TurnOnOff(device.id, false);
            //        }
            //        catch { }
            //        return null;
            //    }
            //}

            if (update!.CallbackQuery!.Data == "Назад")
            {
                userState.Pages.Pop();
                return userState.CurrentPage.View(update, userState);
            }
            return View(update, userState);
        }

        private IReplyMarkup GetReplyKeyboardMarkup(Rootobject rootObject, IEnumerable<Room> rooms)
        {
            var keyboard = new List<List<InlineKeyboardButton>>();

            foreach (var group in rootObject.groups)
            {
                var row = new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData($"Вкл. {group.name}"),
                    InlineKeyboardButton.WithCallbackData($"Выкл. {group.name}")
                };
                keyboard.Add(row);

            }

            //foreach (var device in rootObject.devices)
            //{
            //    var roomName = rooms.FirstOrDefault(room => room.id == device.room).name;
            //    if (roomName == "Зал" || roomName == "Спальня")
            //    {
            //        var row = new List<InlineKeyboardButton>
            //    {
            //        InlineKeyboardButton.WithCallbackData($"Вкл. {device.name} {roomName}"),
            //        InlineKeyboardButton.WithCallbackData($"Выкл. {device.name} {roomName}")
            //    };
            //        keyboard.Add(row);
            //    }
            //}

            var lastRow = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Назад")
            };
            keyboard.Add(lastRow);

            return new InlineKeyboardMarkup(keyboard);
        }


        private IEnumerable<string> GetYandexDevicesTypes(Rootobject rootObject)
        {
            return rootObject.devices.Select(device => device.type).Distinct();
        }
    }
}