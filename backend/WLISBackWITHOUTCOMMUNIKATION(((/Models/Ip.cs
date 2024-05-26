using System.ComponentModel.DataAnnotations;

namespace WLISBackWITHOUTCOMMUNIKATION___.Models
{
    public class Ip
    {
        [Key]
        public Guid Id { get; set; }   
        public string? IpString { get; set; }
    }
}
