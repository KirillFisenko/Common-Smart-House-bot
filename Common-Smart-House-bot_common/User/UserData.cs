namespace Common_Smart_House_bot_common.User
{
    public class UserData
    {
        public int? UserId { get; set; }
        public Message? LastMessage { get; set; }
        public override string ToString()
        {
            return $"{UserId}";
        }
    }
}
