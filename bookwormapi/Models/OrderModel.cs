using System.ComponentModel.DataAnnotations;

namespace bookwormapi.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string OrderDuration { get; set; }

        [Required]
        public int UserId { get; set; }

        public string UserAddress { get; set; }

        public string OrderStatus { get; set; }

        public int NumberOfItems { get; set; }

        public float TotalCost { get; set; }

        public virtual ICollection<OrderItemsModel> OrderItems { get; set; }
    }
}
