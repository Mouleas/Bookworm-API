using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookwormapi.Models
{
    public class ReviewModel
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set;}

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        public string Review { get; set; }
 
        public virtual UserModel User { get; set; }
        public virtual BookModel Book { get; set; }


    }
}
