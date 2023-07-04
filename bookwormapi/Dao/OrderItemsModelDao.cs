
namespace bookwormapi.Dao
{
    public class OrderItemsModelDao
    {
        public int OrderItemsId { get; set; }

        public int OrderId { get; set; }

        public string BookName { get; set; }

        public string BookDescription { get; set; }

        public string BookLanguage { get; set; }

        public string BookAuthor { get; set; }

        public int TotalPages { get; set; }

        public int BookQuantity { get; set; }

        public float BookPrice { get; set; }
        public int PreviousOwnership { get; set; }

        public int PublisherId { get; set; }

    }
}
