
using System.ComponentModel.DataAnnotations;

namespace WLISBackend.models
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<Song> Songs { get; set; } = [];
    }
}
