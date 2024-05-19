namespace WLISBackend.models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MessageText { get; set; } = string.Empty;
    }
}
