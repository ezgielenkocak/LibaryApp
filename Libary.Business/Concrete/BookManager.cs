using Libary.Business.Abstract;
using Libary.Business.Constants;
using LibaryApp.Core.Result;
using LibaryApp.Dal.Abstract;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BookDtos;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Libary.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public IDataResult<Book> AddBook(AddBookDto dto, IFormFile image)
        {
            try
            {
                if (dto == null)
                {
                    return new ErrorDataResult<Book>(null, "Data is null", Messages.err_null);
                }
                if (image != null && image.Length > 0)
                {
                    // Dosya adını ve yolunu oluşturdum
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                    // Dosya yükleme işlemi
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    // AddBookDto verilerini kullanarak Book nesnesi oluşturdum
                    var book = new Book
                    {
                        BookName = dto.BookName,
                        Author = dto.Author,
                        CreatedDate = DateTime.Now,
                        Image = uniqueFileName, // Resim dosyasının adını veri tabanına kaydedeceğim
                        InLibary = true
                    };

                    // Book nesnesini veritabanına ekledim
                    _bookDal.Add(book);
                    return new SuccessDataResult<Book>(book, "Ok", Messages.success);

                }
                return new ErrorDataResult<Book>(null, "err", Messages.unk_err);
            
            }
            catch (Exception e)
            {

                return new ErrorDataResult<Book>(null, e.Message, Messages.unk_err);
            }
        }

        public IDataResult<Book> Get(Expression<Func<Book, bool>> filter)
        {
            try
            {
                var getBooks = _bookDal.Get(filter);
                if (getBooks == null)
                {
                    return new ErrorDataResult<Book>(null, "book not found", Messages.book_not_found);
                }
                return new SuccessDataResult<Book>(getBooks, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<Book>(null, e.Message, Messages.unk_err);
            }
        }

        public IDataResult<Book> GetBookById(int id)
        {
            try
            {
                if (id == null)
                {
                    return new ErrorDataResult<Book>(null, "id is not be null", Messages.err_null);
                }
                var getBook = _bookDal.Get(x => x.Id == id);
                if (getBook == null)
                {
                    return new ErrorDataResult<Book>(null, "book_not_found", Messages.book_not_found);
                }
                return new SuccessDataResult<Book>(getBook, "Ok", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<Book>(null, e.Message, Messages.unk_err);
            }
        }

        public IDataResult<List<ListBookDto>> GetBooks()
        {
            try
            {
                var books = _bookDal.GetList().OrderBy(x => x.BookName).ToList();
                int bookNumber = 1;
                var booksListDto = new List<ListBookDto>();
                foreach (var book in books)
                {
                    string state = book.InLibary ? "Kütüphanede" : "Dışarıda";

                    booksListDto.Add(new ListBookDto
                    {
                        Id = book.Id,
                        BookName = book.BookName,
                        Author = book.Author,
                        Number = bookNumber,
                        Image = book.Image,
                        InLibary = state

                    });
                    bookNumber++;
                }
                return new SuccessDataResult<List<ListBookDto>>(booksListDto, "success", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<ListBookDto>>(null, e.Message, Messages.unk_err);
            }
        }

        public IDataResult<List<ListBookDto>> GetBooksOutside()
        {
            try
            {
                var books = _bookDal.GetList().OrderBy(x => x.BookName).Where(y => y.InLibary == false).ToList();
                int bookNumber = 1;
                var booksListDto = new List<ListBookDto>();
                foreach (var book in books)
                {
                    string state = book.InLibary ? "Kütüphanede" : "Dışarıda";
                    booksListDto.Add(new ListBookDto
                    {
                        BookName = book.BookName,
                        Author = book.Author,
                        Number = bookNumber,
                        Image = book.Image,
                        InLibary = state

                    });
                    bookNumber++;
                }
                return new SuccessDataResult<List<ListBookDto>>(booksListDto, "success", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<ListBookDto>>(null, e.Message, Messages.unk_err);
            }
        }
    }
}
