namespace WLISBackend.models
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<string> Songs { get; set; } = [];

    }
}
