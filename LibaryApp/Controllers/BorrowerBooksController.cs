using Libary.Business.Abstract;
using LibaryApp.Entity.Dtos.BorrowerBookDtos;
using Microsoft.AspNetCore.Mvc;

namespace LibaryApp.Controllers
{
    public class BorrowerBooksController : Controller
    {
        private readonly IBorrowerBooksService _borrowerBooksService;

        public BorrowerBooksController(IBorrowerBooksService borrowerBooksService)
        {
            _borrowerBooksService = borrowerBooksService;
        }

        [HttpGet]
        public IActionResult GetBorrowerBooksList()
        {
            var result=_borrowerBooksService.GetBorrowerBooksList();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddBorrowerBook(int bookId)
        {
            AddBorrowerBooksDto dt = new()
            {
                BookId = bookId
            };
            return View(dt);
        }
        [HttpPost]
        public IActionResult AddBorrowerBook(AddBorrowerBooksDto addBorrowerBooksDto)
        {
            if (!ModelState.IsValid)
            {
                return View("AdBorrowerBook");
            }
            var result = _borrowerBooksService.AddBorrowerBook(addBorrowerBooksDto);
            return RedirectToAction("GetBorrowerBooksList");
        }

        [HttpGet]
        public IActionResult GetBorrowerBookGetById(int id)
        {
            var result = _borrowerBooksService.GetBorrowerBookGetById(id);
            return View(result.Data);   
        }
      
    }
}
