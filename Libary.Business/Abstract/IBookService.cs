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
        IDataResult<Book> AddBook(AddBookDto dto, IFormFile image); //Kitap ekler
        IDataResult<List<ListBookDto>> GetBooks(); //Anasayfadaki kitap bilgileri-ödünç alan- getireceği tarihi döner
        IDataResult<Book> Get(Expression<Func<Book, bool>> filter); // linq expression yazarken kullanıyorum
        IDataResult<Book> GetBookById(int id); //Ödünç ver kısmında kitabın id'sini otomatik olarak almamı sağlıyor
        IDataResult<List<Book>> GetActiveBooks(); //sadece kütüphanede bulunan kitapları listeler.

    }
}
