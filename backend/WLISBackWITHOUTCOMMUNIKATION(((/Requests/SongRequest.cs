using WLISBackend.models;

namespace WLISBackend.requests
{
    public record SongRequest
    (
        string Title,
        List<Guid> ArtistsId,
        Guid AlbumId
    );
}
