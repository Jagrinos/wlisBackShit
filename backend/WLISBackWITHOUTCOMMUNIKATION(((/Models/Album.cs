using System.ComponentModel.DataAnnotations;

namespace WLISBackend.models
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Song> Songs { get; set; } = [];

    }
}
