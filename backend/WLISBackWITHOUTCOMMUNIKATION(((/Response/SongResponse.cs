using WLISBackend.models;

namespace WLISBackWITHOUTCOMMUNIKATION___.Response
{
    public class SongResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public List<ArtistResponse>? Artists { get; set; } 
        public AlbumResponse? Album { get; set; }
    }
}
