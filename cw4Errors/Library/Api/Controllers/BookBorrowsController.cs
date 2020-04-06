using System.Threading.Tasks;
using Library.Models.DTO;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Controllers
{
    [Route("api/book-borrows")]
    //[Authorize(Roles="Reader")]
    public class BookBorrowsController : Controller
    {
        private readonly IBookBorrowRepository _bookBorrowRepository;
        private readonly ILogger<BookBorrowsController> _logger;

        public BookBorrowsController(IBookBorrowRepository bookBorrowRepository, ILogger<BookBorrowsController> logger)
        {
            _logger = logger;
            _bookBorrowRepository = bookBorrowRepository;
        }


        [HttpPost(Name = nameof(AddBookBorrow))]
        public async Task<IActionResult> AddBookBorrow([FromBody] BookBorrowDto borrow)
        {
            var res = await _bookBorrowRepository.AddBookBorrow(borrow);
            return CreatedAtRoute(nameof(AddBookBorrow), res);
        }

        [HttpPut("{idBookBorrow}")]
        public async Task<IActionResult> UpdateBookBorrow([FromBody] UpdateBookBorrowDto borrow)
        {
            await _bookBorrowRepository.ChangeBookBorrow(borrow);
            return NoContent();
        }

        
    }
}