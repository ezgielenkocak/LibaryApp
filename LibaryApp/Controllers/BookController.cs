using FluentValidation.Results;
using Libary.Business.Abstract;
using Libary.Business.ValidationRules;
using LibaryApp.Core.Entities;
using LibaryApp.Entity.Dtos.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var result = _bookService.GetBooks();
            return View(result.Data);
        }
        public IActionResult GetBooksOutside()
        {
            var result = _bookService.GetBooksOutside();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult AddNews()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddBookDto dto, IFormFile image)
        {
            BookValidator bookValidator = new BookValidator();
            ValidationResult results = bookValidator.Validate(dto);
            if (results.IsValid)
            {
                var result = _bookService.AddBook(dto, image);
                return View(result.Data);
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    string message = item.ErrorMessage;
                    string keys = item.PropertyName;
                    ModelState.AddModelError(keys, message);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var result = _bookService.GetBookById(id);
            return View("GetBookById", result.Data);
        }
    }
}
