
namespace bookwormapi.Dao
{
    public class OrderItemsModelDao
    {
        public int OrderItemsId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }
    }
}
