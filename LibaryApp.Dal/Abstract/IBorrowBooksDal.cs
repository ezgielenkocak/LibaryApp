using LibaryApp.Core.Repository;
using LibaryApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Dal.Abstract
{
    public interface IBorrowBooksDal:IEntityRepository<BorrowerBook>
    {
    }
}
