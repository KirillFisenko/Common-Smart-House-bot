using Telegram.Bot.Types;

namespace Common_Smart_House_bot.Services
{
    public class ResourcesService
    {
        public static InputFileStream GetResources(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var filename = path.Split("\\").Last();
            return InputFile.FromStream(fileStream, filename);
        }
    }
}
