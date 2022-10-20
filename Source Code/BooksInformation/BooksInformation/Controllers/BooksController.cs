using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper; 
using BooksInformation.Models;
using BooksInformation.Repository;
using BooksInformation.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksInformation.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        //private readonly IBookDataStore _iBookDataStore;
        private readonly IBookInfoRepository _bookinforepository;
        private readonly ILogger<BooksController> _logger;
        private readonly IMapper _mapper;

        public BooksController(IBookInfoRepository bookinforepository, ILogger<BooksController> logger, IMapper mapper)
        {
            //_iBookDataStore = iBookDataStore;
            _mapper = mapper;
            _bookinforepository = bookinforepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            _logger.LogInformation($"Method Invoked GetBooks()");
            _logger.LogInformation($"Exiting from Method GetBooks()");

            var BookEntities = await _bookinforepository.GetBooksAsync();

            return Ok(BookEntities);

            //return Ok(_iBookDataStore.GetData());             

        }

        

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            _logger.LogInformation($"Method Invoked GetBook(string id)"); 

            if(string.IsNullOrEmpty(id))
            {
                _logger.LogInformation($"Received Invalid ID :  {id}");
                return BadRequest("Invalid ID");
            }

            var tempdata = await _bookinforepository.GetBookAsync(id);

            if (tempdata == null)
            {
                _logger.LogInformation($"No Books found with the given ID {id}");
                return NotFound();
            }
            _logger.LogInformation($"Exiting from Method GetBook(string id)");

            return Ok(tempdata);

        }
         
        [HttpPost]
        public async Task<ActionResult<Book>> CreateNewBook(BookCreation _book)
        {
            _logger.LogInformation($"Method Invoked CreateNewBook(BookCreation _book)");

            var book = _mapper.Map<Models.Book>(_book);

            await _bookinforepository.CreateNewBookAsync(book);
            await _bookinforepository.SaveChangesAsync();
             
            _logger.LogInformation($"New Book Cretd successfully with Book Name  {_book.name}, Book Author {_book.authoName} and the New ID {book.ID}.");
            _logger.LogInformation($"Exiting from Method CreateNewBook(BookCreation _book)");

            return CreatedAtRoute("GetBook", new { id = book.ID }, book);
        }  
         
    }
}

