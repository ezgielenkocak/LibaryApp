using Libary.Business.Abstract;
using Libary.Business.Constants;
using LibaryApp.Core.Result;
using LibaryApp.Dal.Abstract;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BorrowerBookDtos;
using System.Globalization;

namespace Libary.Business.Concrete
{
    public class BorrowerBooksManager : IBorrowerBooksService
    {
        private readonly IBorrowBooksDal _borrowerBooksDal;
        private readonly IBookDal _booksDal;

        public BorrowerBooksManager(IBorrowBooksDal borrowerBooksDal, IBookDal booksDal)
        {
            _borrowerBooksDal = borrowerBooksDal;
            _booksDal = booksDal;
        }

        //Kitap ödünç verme işlemi
        public IDataResult<BorrowerBook> AddBorrowerBook(AddBorrowerBooksDto addBorrowerBooksDto)
        {
            try
            {
                if (addBorrowerBooksDto == null)
                {
                    return new ErrorDataResult<BorrowerBook>(null, "data is null", Messages.err_null);
                }
                var bookAndBorrowerMatch = _booksDal.Get(x => x.Id == addBorrowerBooksDto.BookId);

                    var borrowerBook = new BorrowerBook
                {
                    BorrowersName = addBorrowerBooksDto.BorrowersName,
                    ReturnDate = addBorrowerBooksDto.ReturnDate,
                    BookId = addBorrowerBooksDto.BookId
                };
                _borrowerBooksDal.Add(borrowerBook);
                // Yukarıda Kitap ödünç verme işlemini yaptıktan sonra Kitap durumunu dışarıda olarak güncelliyorum böylece dışarıdaki kitabın tekrar ödünç verilmesini engelliyorum.
                var book = new Book
                {
                    Id = bookAndBorrowerMatch.Id,
                    Author = bookAndBorrowerMatch.Author,
                    BookName = bookAndBorrowerMatch.BookName,
                    Image = bookAndBorrowerMatch.Image,
                    InLibary = false,
                    CreatedDate = bookAndBorrowerMatch.CreatedDate,
                };
                _booksDal.Update(book);
                return new SuccessDataResult<BorrowerBook>(borrowerBook);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<BorrowerBook>(null, e.Message, Messages.unk_err);
            }
        }

      //Ödünç verilen kitaplar listesi
        public IDataResult<List<ListBorrowerBooksDto>> GetBorrowerBooksList()
        {
            try
            {
                var borrowBooks = _borrowerBooksDal.GetList().OrderBy(x => x.BorrowersName); // kitap ismine göre alfabetik olarak ödünç verilen kitapları çekiyorum.
                var borrowBooksListDto = new List<ListBorrowerBooksDto>();
                int columnNumber = 1; //alfabetik sıralama yaptığım için id'yi göstermek yerine bir nevi indexleme görevi gören bu değişkeni tanımladım
                foreach (var item in borrowBooks)
                {
                    var book = _booksDal.Get(x => x.Id == item.BookId);

                    borrowBooksListDto.Add(new ListBorrowerBooksDto
                    {
                        Number = columnNumber,
                        BookName = book.BookName,
                        BorrowersName = item.BorrowersName,
                        ReturnDate = item.ReturnDate,
                    });
                    columnNumber++;

                }
                return new SuccessDataResult<List<ListBorrowerBooksDto>>(borrowBooksListDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<ListBorrowerBooksDto>>(null, e.Message, Messages.unk_err);
            }
        }
    }
}
