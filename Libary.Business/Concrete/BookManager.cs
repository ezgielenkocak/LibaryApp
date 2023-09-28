using Libary.Business.Abstract;
using Libary.Business.Constants;
using LibaryApp.Core.Result;
using LibaryApp.Dal.Abstract;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.Concrete
{
    public class BookManager : IBookService
    {
		private readonly IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public IDataResult<Book> AddBook(AddBookDto dto)
        {
			try
			{
				if (dto==null)
				{
					return new ErrorDataResult<Book>(null, "Data is null", Messages.err_null);
				}
				var book = new Book 
				{
					BookName=dto.BookName,
					Author=dto.Author,
					CreatedDate=DateTime.Now,
					Image=dto.Image,	
					InLibary=true
				};
				_bookDal.Add(book);
				return new SuccessDataResult<Book>(book, "Ok", Messages.success);
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
				var books = _bookDal.GetList().OrderBy(x=>x.BookName).ToList();
				int bookNumber=1;
				var booksListDto = new List<ListBookDto>();
				foreach (var book in books)
				{
					string state = book.InLibary ? "Kütüphanede" : "Rezerve";

					booksListDto.Add(new ListBookDto
					{
						BookName = book.BookName,
						Author = book.Author,
						Number = bookNumber,
						Image = book.Image,
						InLibary=state

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
