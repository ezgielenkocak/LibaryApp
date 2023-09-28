using LibaryApp.Core.Result;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.Abstract
{
    public interface IBookService
    {
        IDataResult<Book> AddBook(AddBookDto dto);
        IDataResult<List<ListBookDto>> GetBooks();
    }
}
