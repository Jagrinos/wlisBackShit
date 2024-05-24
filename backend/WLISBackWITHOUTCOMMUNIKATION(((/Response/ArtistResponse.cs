using WLISBackend.models;

namespace WLISBackWITHOUTCOMMUNIKATION___.Response
{
    public class ArtistResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } 
        public string? Role { get; set; }
        
        public List<SongResponse>? Songs { get; set; } 
    };
}
