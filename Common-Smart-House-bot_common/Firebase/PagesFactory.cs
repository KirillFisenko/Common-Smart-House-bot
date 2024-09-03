using Common_Smart_House_bot_common.User.Pages;
using System.Reflection;

namespace Common_Smart_House_bot_common.Firebase
{
    public static class PagesFactory
    {
        public static IPage GetPage(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == name && typeof(IPage).IsAssignableFrom(t));
            return (IPage)Activator.CreateInstance(type);
        }
    }
}
