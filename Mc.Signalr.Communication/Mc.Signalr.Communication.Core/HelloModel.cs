namespace Mc.Signalr.Communication.Server.Model
{
    public class HelloModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        //Zorunlu değil ama güvenlik için kullanabiliriz. Gönderen kişi bir kod üretir. Alıcıda bu kodu üretip verinin değişmediğini onaylar.
        public string HashCode { get; set; }
    }
    public static class HubSetting
    {
        public static string HubName = "ChannelHub";
        public static string HubUrl
        {
            get
            {
                return "http://localhost:8089";
            }
        }
    }
}
