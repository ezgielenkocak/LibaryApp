using LibaryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Entity.Dtos.BorrowerBookDtos
{
    public class AddBorrowerBooksDto:IDto
    {
        [Required]
        public string BorrowersName { get; set; }
        public int BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
