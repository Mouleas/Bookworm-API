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

        [ForeignKey("Book")]
        public int ProductId { get; set; }

        [Required]
        public int ProductQuantity { get; set; }

        public virtual OrderModel Order { get; set; }

        public virtual BookModel Book { get; set; }

    }
}
