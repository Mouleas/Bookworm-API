using System.ComponentModel.DataAnnotations;

namespace bookwormapi.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserPassword { get; set; }
        public float UserAccount { get; set; }
    }
}
