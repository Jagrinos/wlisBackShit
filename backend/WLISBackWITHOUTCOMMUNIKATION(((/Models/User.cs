using System.ComponentModel.DataAnnotations;

namespace WLISBackWITHOUTCOMMUNIKATION___.Auntefication
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
