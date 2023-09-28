using Libary.Business.Abstract;
using LibaryApp.Entity.Dtos.BookDtos;
using Microsoft.AspNetCore.Mvc;

namespace LibaryApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult GetAll()
        {
            var result=_bookService.GetBooks();   
            return View(result.Data);
        }
        public IActionResult Add(AddBookDto dto)
        { 
            var result=_bookService.AddBook(dto);
            return View(result.Data);
        }
    }
}
