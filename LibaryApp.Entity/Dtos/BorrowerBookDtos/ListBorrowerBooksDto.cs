using LibaryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Entity.Dtos.BorrowerBookDtos
{
    public class ListBorrowerBooksDto:IDto
    {
        public int Id { get; set; }
        public string BorrowersName { get; set; }
        public string BookName { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
