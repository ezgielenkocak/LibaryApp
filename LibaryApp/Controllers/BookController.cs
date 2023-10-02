using FluentValidation.Results;
using Libary.Business.Abstract;
using Libary.Business.ValidationRules;
using LibaryApp.Core.Entities;
using LibaryApp.Core.Result;
using LibaryApp.Entity.Dtos.BookDtos;
using LibaryApp.Entity.Dtos.BorrowerBookDtos;
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
  
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        } 
        [HttpPost]
        public IActionResult Add(AddBookDto dto, IFormFile image) //Kitap ekleme controller'ı
        {
            var result = _bookService.AddBook(dto, image);
            if (result.Data ==null)
            {
                return View(dto);
            }
            return RedirectToAction("GetAll");
        } 


        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var result = _bookService.GetBookById(id);
            return View("GetBookById", result.Data);
        } //Ödünç verme işleminde otomatik olarak BookId'yi otomatik almamı sağlayan controllerım

        public IActionResult GetActiveBooks()
        {
            var result = _bookService.GetActiveBooks();
            return View(result.Data);
        } //Kütüphanede aktif olan kitapları getiren Controller
    }
}
