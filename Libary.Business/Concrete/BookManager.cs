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
        private readonly IBorrowBooksDal _borrowBooksDal;
        public BookManager(IBookDal bookDal, IBorrowBooksDal borrowBooksDal)
        {
            _bookDal = bookDal;
            _borrowBooksDal = borrowBooksDal;
        }

        //Kitap Ekleme İşlemi
        public IDataResult<Book> AddBook(AddBookDto dto, IFormFile image)
        {
            try
            {
                if (dto == null || image==null) //nesnelerin boş gelme durumunu kontrol ettim.
                {
                    return new ErrorDataResult<Book>(null, "Data is null", Messages.err_null);
                }
                if (image.Length > 0)
                {
                    // Dosya adını ve yolunu oluşturdum
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName); //eklenen kitap resimlerinin roota eklenmesini sağladım

                    // Dosya yükleme işlemini yaptım
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    //mapleme işlemim
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
        } //diğer servisten veri çekerken kullandığım metodum

        //Sadece kütüphanede bulunan kitapları(ödünç verilenleri görmüyor) listeleme işlemi
        public IDataResult<List<Book>> GetActiveBooks()
        {
            try
            {
                var getActiveBooks = _bookDal.GetList().Where(x => x.InLibary == true ).OrderBy(x=>x.BookName).ToList(); //sadece kütüphanede olan kitapları getiriyorum. Alfabetik olarak yapmadım çünkü bu sefer kitap id'lerini göstermek istedim.
                if (getActiveBooks==null)
                {
                    return new ErrorDataResult<List<Book>>(null, "err_null", Messages.err_null);
                }
                return new SuccessDataResult<List<Book>>(getActiveBooks, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<Book>>(null, e.Message, Messages.unk_err);

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
        } //Ödünç verme işleminde kitap id'sinin otomatik olarak gitmesini sağlayan metodum

        #region GetBooks Açıklama
        /* Anasayfada gözüken kitap ismi, yazarı, resmi, ödünç alanıın ismi, geri getireceği tarih bilgilerini içeren iki tablodan veri çekip listelediğim metot
         */
        #endregion
        public IDataResult<List<ListBookDto>> GetBooks()
        {
            try
            {
                #region books Değişkeni açıklama
                //Kitapları alfabetik sıraya göre çektim
                #endregion
                var books = _bookDal.GetList().OrderBy(x => x.BookName).ToList();

                #region bookNumber değişkeni açıklama
                // alfabetik sıralama yaptığım için db'den gelen Id'yi değil 1 den başlayıp aritmetik artan birnevi index görevi gören değişkenim.
                #endregion
                int bookNumber = 1; 
                var booksListDto = new List<ListBookDto>();
                if (books!=null)
                {
                    foreach (var book in books)
                    {
                        #region state değişkeni açıklama
                        // InLibary'yi true false yerine bu şekilde kullanmayı tercih ettim
                        #endregion
                        string state = book.InLibary ? "Kütüphanede" : "Dışarıda";

                        #region borrowBook değişkeni açıklama
                        //BorrowersBook(Ödünç Alınan kitaplar) tablosundaki Ödünç alan ve Geri getirme tarihini Books tablosuyla birleştirmek için yaptığım işlem
                        #endregion
                        var borrowBook = _borrowBooksDal.Get(x => x.BookId == book.Id);
                        if (borrowBook == null)
                        {
                            if (book.InLibary==false)
                            {
                                BookAndBorroweBookControl(book);

                            }
                            booksListDto.Add(new ListBookDto
                            {
                                Id = book.Id,
                                BookName = book.BookName,
                                Author = book.Author,
                                Number = bookNumber,
                                Image = book.Image,
                                InLibary = state,

                            });
                        }
                        else
                        {
                            booksListDto.Add(new ListBookDto
                            {
                                Id = book.Id,
                                BookName = book.BookName,
                                Author = book.Author,
                                Number = bookNumber,
                                Image = book.Image,
                                InLibary = state,
                                BorrowersName = borrowBook.BorrowersName,
                                ReturnDate = borrowBook.ReturnDate,
                            });
                        }
                        bookNumber++; //indexleme görevi gören değişkenim
                    }
                    return new SuccessDataResult<List<ListBookDto>>(booksListDto, "success", Messages.success);

                }


                return new ErrorDataResult<List<ListBookDto>>(null, "null", Messages.err_null);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<ListBookDto>>(null, e.Message, Messages.unk_err);
            }
        }


        #region BookAndBorroweBookControl Açıklama
        //Silme ve güncelleme işlemlerinin logic'iğini yazmadığım için mesela Ödünç alınan kitaplar(BorrowerBook table) tablosuna truncate atıp Books tablosunda değişiklik yapmayı unutursam otomatik olarak Books tablosundaki InLibary(yani kütüphanede mi dışarıda mı ) değerini true'ya çekiyorum ki veri kaybı yaşanmasın.   (GetBooks metodunda kullandım)
        #endregion
        public bool BookAndBorroweBookControl(Book book)
        {
            var checkBorrowerBookCount = _borrowBooksDal.GetList();
            if (checkBorrowerBookCount.Count<=0)
            {
                var bookControl = new Book()
                {
                    Author = book.Author,
                    BookName = book.BookName,
                    Id = book.Id,
                    Image = book.Image,
                    InLibary = true,
                    CreatedDate=book.CreatedDate,
                };
                _bookDal.Update(bookControl);  
            }
            return true;
        }
    }
}
