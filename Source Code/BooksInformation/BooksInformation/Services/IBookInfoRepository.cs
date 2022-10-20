using System;
using BooksInformation.Models;

namespace BooksInformation.Services
{
    public interface IBookInfoRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();

        Task<Book?> GetBookAsync(string ID);

        Task CreateNewBookAsync(Book book);

        Task<bool> SaveChangesAsync();
    }
}

