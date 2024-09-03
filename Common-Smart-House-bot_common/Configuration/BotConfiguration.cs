namespace Common_Smart_House_bot_common.Configuration
{
    public class BotConfiguration
    {
        public const string SectionName = "BotConfiguration";
        public const string UpdateRoute = "/webhook/update";
        public string BotToken { get; set; }
        public string HostAddress { get; set; }
        public string SecretToken { get; set; }
    }
}
