using System.ComponentModel.DataAnnotations;

namespace bookwormapi.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string BookDescription { get; set; }

        [Required]
        public string BookLanguage { get; set; }

        [Required]
        public string BookAuthor { get; set; }

        [Required]
        public float BookPrice { get; set; }

        [Required]
        public int BookQuantity { get; set; }

        [Required]
        public int PreviousOwnership { get; set; }

        [Required]
        public int TotalPages { get; set; }

        public string BookImg { get; set; }

        public virtual ICollection<ReviewModel> Reviews { get; set; }
        public virtual ICollection<CartModel> Carts { get; set; }
        public virtual ICollection<OrderItemsModel> OrderItems { get; set; }


    }
}
