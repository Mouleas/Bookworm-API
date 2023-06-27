using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookwormapi.Models
{
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        public int BookQuantity { get; set; }

        public virtual UserModel User { get; set; }
        public virtual BookModel Book { get; set; }

    }
}
