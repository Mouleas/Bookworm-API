﻿using Microsoft.EntityFrameworkCore;
using bookwormapi.Models;

namespace bookwormapi.Data
{
    public class BookwormContext : DbContext
    {
        public BookwormContext(DbContextOptions<BookwormContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasMany(r => r.Reviews)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<BookModel>()
                .HasMany(r => r.Reviews)
                .WithOne(b => b.Book)
                .HasForeignKey(b => b.BookId);


            modelBuilder.Entity<UserModel>()
                .HasMany(c => c.Carts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<BookModel>()
                .HasMany(c => c.Carts)
                .WithOne(b => b.Book)
                .HasForeignKey(b => b.BookId);

        }
        public DbSet<bookwormapi.Models.BookModel>? BookModel { get; set; }
        public DbSet<bookwormapi.Models.UserModel>? UserModel { get; set; }
        public DbSet<bookwormapi.Models.ReviewModel>? ReviewModel { get; set; }
        public DbSet<bookwormapi.Models.CartModel>? CartModel { get; set; }


    }
}
