using WLISBackend.models;

namespace WLISBackend.requests
{
    public record ArtistRequest
    (
        string Name,
        string Role,
        List<string> Songs
    );
}
