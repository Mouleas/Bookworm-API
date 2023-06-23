using Microsoft.EntityFrameworkCore;
using bookwormapi.Models;

namespace bookwormapi.Data
{
    public class BookwormContext : DbContext
    {
        public BookwormContext(DbContextOptions<BookwormContext> options)
            : base(options){}
        public DbSet<bookwormapi.Models.BookModel>? BookModel { get; set; }
        public DbSet<bookwormapi.Models.UserModel>? UserModel { get; set; }


    }
}
