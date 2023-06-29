namespace bookwormapi.Dao
{
    public class OrderModelDao
    {
        public int OrderId { get; set; }
        
        public string OrderDuration { get; set; }

        public int UserId { get; set; }

        public string UserAddress { get; set; }

        public string OrderStatus { get; set; }

        public int NumberOfItems { get; set; }

        public float TotalCost { get; set; }
    }
}
