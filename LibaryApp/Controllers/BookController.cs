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
        public IActionResult GetBooksOutside()
        {
            var result=_bookService.GetBooksOutside();  
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddBookDto dto, IFormFile image)
        { 
            var result=_bookService.AddBook(dto, image);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var result=_bookService.GetBookById(id);
            return View("GetBookById",result.Data);
        }
    }
}
