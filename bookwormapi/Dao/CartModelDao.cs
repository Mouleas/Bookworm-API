namespace bookwormapi.Dao
{
    public class CartModelDao
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int BookQuantity { get; set; }
    }
}
