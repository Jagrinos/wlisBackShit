using WLISBackend.models;

namespace WLISBackWITHOUTCOMMUNIKATION___.Response
{
    public class AlbumResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public List<SongResponse>? Songs { get; set; } 
    }
}
