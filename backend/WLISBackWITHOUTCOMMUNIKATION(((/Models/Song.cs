using System.ComponentModel.DataAnnotations;

namespace WLISBackend.models
{
    public class Song
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<Artist> Artists { get; set; } = [];

        public Guid AlbumId { get; set; }
        public Album Album { get; set; }

    }
}
