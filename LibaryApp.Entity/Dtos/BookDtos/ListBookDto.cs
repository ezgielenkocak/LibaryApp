using LibaryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Entity.Dtos.BookDtos
{
    public class ListBookDto:IDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public string InLibary { get; set; }

        //Ödünç alanın ismi ve geri getireceği tarih.
        public string? BorrowersName { get; set; } 
        public DateTime? ReturnDate { get; set; }
    }
}
