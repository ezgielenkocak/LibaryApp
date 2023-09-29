using LibaryApp.Core.Result;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BookDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.Abstract
{
    public interface IBookService
    {
        IDataResult<Book> AddBook(AddBookDto dto, IFormFile image);
        IDataResult<List<ListBookDto>> GetBooks();
        IDataResult<List<ListBookDto>> GetBooksOutside();
        IDataResult<Book> Get(Expression<Func<Book, bool>> filter);
        IDataResult<Book> GetBookById(int id);
        
    }
}
