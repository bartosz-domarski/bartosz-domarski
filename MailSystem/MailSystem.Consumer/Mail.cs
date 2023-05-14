namespace MailSystem.Consumer
{
    public class Mail
    {
        public string Message { get; set; } = default!;
        public MailType MailType { get; set; }
    }
}
