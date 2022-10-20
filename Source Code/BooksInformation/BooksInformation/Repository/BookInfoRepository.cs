using System;
using BooksInformation.DbContexts;
using BooksInformation.Models;
using BooksInformation.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksInformation.Repository
{
    public class BookInfoRepository : IBookInfoRepository
    { 
        private readonly BookInfoContext _context;

        public BookInfoRepository(BookInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateNewBookAsync(Book book)
        {
            await _context.book.AddAsync(book);
            //return await _context.Add(book);
            //throw new NotImplementedException();

             
        }

        public async Task<Book?> GetBookAsync(string ID)
        {
            return await _context.book.Where(b => b.ID == ID).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.book.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}

