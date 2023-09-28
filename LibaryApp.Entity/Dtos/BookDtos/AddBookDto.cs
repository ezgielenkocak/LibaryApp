using LibaryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Entity.Dtos.BookDtos
{
    public class AddBookDto :IDto
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public bool InLibary { get; set; }
    }
}
