using WLISBackend.models;

namespace WLISBackend.requests
{
    public record SongRequest
    (
        string Title,
        List<string> Artists,
        List<string> Albums
    );
}
