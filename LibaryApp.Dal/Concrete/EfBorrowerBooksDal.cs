using LibaryApp.Core.EntityFramework;
using LibaryApp.Dal.Abstract;
using LibaryApp.Dal.Context;
using LibaryApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Dal.Concrete
{
    public class EfBorrowerBooksDal:EfEntityRepositoryBase<BorrowerBook, LibaryContext>, IBorrowBooksDal
    {
    }
}
