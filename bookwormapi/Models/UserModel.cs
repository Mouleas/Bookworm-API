using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace bookwormapi.Models
{
    [Index(nameof(UserEmail), IsUnique = true)]
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

        public virtual ICollection<ReviewModel> Reviews { get; set; }
        public virtual ICollection<CartModel> Carts { get; set; }
    }
}
