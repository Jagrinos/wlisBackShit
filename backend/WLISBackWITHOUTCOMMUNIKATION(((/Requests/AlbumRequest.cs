namespace WLISBackWITHOUTCOMMUNIKATION___.requests
{
    public record AlbumRequest
    (
        string Title,
        string Description,
        List<string> Songs 
    );
}
