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
        public IActionResult GetBorrowerBooksList() //Ödünç verilen kitaplar listesi controller'ı
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
            //Bugünün tarihinden daha küçük bir Geri Getirme Tarihi girilmemesi için yaptığım kontrol
            if (addBorrowerBooksDto.ReturnDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("ReturnDate", "Geri getirme tarihi bugünden küçük olamaz.");
                return View(addBorrowerBooksDto);
            }
            var result = _borrowerBooksService.AddBorrowerBook(addBorrowerBooksDto);
            return RedirectToAction("GetBorrowerBooksList");
        }

    }
}
