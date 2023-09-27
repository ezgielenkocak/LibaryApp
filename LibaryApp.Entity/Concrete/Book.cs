using LibaryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Entity.Concrete
{
    public class Book:IEntity
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
