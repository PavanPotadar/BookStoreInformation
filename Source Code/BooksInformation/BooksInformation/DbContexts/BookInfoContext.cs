using System;
using BooksInformation.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksInformation.DbContexts
{
    public class BookInfoContext : DbContext
    {
        public DbSet<Book> book { get; set; } = null!;

        public BookInfoContext(DbContextOptions<BookInfoContext> options) : base(options)
        {

        }
    }
}

