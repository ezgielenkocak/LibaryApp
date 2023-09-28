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
            optionsBuilder.UseSqlServer("server=DESKTOP-VTNRLAJ; database=LibaryDb; integrated security=true");
        }
       public DbSet<Book> Books { get; set; }
    }
}
