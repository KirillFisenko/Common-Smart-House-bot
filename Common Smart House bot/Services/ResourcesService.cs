using Telegram.Bot.Types;

namespace Common_Smart_House_bot_common.Services
{
    public class ResourcesService
    {
        public static InputFileStream GetResources(byte[] file)
        {
            var fileStream = new MemoryStream(file);
            return InputFile.FromStream(fileStream);
        }
    }
}