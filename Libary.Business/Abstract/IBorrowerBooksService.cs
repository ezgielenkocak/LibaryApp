using LibaryApp.Core.Result;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BorrowerBookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.Abstract
{
    public interface IBorrowerBooksService
    {
        IDataResult<BorrowerBook> AddBorrowerBook(AddBorrowerBooksDto addBorrowerBooksDto);
        IDataResult<List<ListBorrowerBooksDto>> GetBorrowerBooksList();
        IDataResult<BorrowerBook> GetBorrowerBookGetById(int id);
    }
}
