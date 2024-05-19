namespace WLISBackWITHOUTCOMMUNIKATION___.Auntefication
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
