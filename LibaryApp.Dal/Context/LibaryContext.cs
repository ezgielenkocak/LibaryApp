using LibaryApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryApp.Dal.Context
{
    public class LibaryContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("@\"Server=DESKTOP-VTNRLAJ; Database=LibaryDb");
        }
       public DbSet<Book> Books { get; set; }
    }
}
