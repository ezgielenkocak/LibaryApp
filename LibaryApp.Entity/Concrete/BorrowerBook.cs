using LibaryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Entity.Concrete
{
    public class BorrowerBook:IEntity
    {
        public int Id { get; set; }
        public string BorrowersName { get; set; }
        public int BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
