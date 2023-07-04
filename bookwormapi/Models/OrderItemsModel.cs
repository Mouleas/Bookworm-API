using bookwormapi.Dao;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookwormapi.Models
{
    public class OrderItemsModel
    {
        [Key] 
        public int OrderItemsId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public string BookName { get; set; }

        public string BookDescription { get; set; }

        public string BookLanguage { get; set; }

        public string BookAuthor { get; set; }

        public int TotalPages { get; set; }

        [Required]
        public int BookQuantity { get; set; }

        public float BookPrice { get; set; }

        public int PreviousOwnership { get; set; }

        public int PublisherId { get; set; }

        public virtual OrderModel Order { get; set; }


    }
}
