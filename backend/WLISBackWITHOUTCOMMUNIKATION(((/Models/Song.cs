namespace WLISBackend.models
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<string> Artists { get; set; } = [];
        public List<string> Albums { get; set; } = [];

    }
}
