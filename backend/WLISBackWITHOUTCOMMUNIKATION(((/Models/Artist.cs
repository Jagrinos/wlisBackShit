
namespace WLISBackend.models
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<string> Songs { get; set; } = [];
    }
}
