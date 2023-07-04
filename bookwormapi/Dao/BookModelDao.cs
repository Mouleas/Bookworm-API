namespace bookwormapi.Dao
{
    public class BookModelDao
    {
        public string BookName { get; set; }
        public string BookDescription { get; set; }

        public string BookLanguage { get; set; }

        public string BookAuthor { get; set; }
        public float BookPrice { get; set; }

        public int BookQuantity { get; set; }
        public int PreviousOwnership { get; set; }

        public int PublisherId { get; set; }

        public int TotalPages { get; set; }

        //public string BookImg { get; set; }
    }
}
