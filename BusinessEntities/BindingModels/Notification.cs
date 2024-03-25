namespace BusinessEntities.BindingModels
{
    public class Notification
    {
        public string SenderId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ReceiverId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
    }
}
