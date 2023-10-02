using Autofac;
using Libary.Business.Abstract;
using Libary.Business.Concrete;
using LibaryApp.Dal.Abstract;
using LibaryApp.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.DependencyResolvers
{
    public class AutofacBusinessModule:Module
    {
        //Dependency Injection işleminin düzgün çalışması için bağımlılıkları programa bildirdim.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfBookDal>().As<IBookDal>();
            builder.RegisterType<BookManager>().As<IBookService>();

            builder.RegisterType<EfBorrowerBooksDal>().As<IBorrowBooksDal>();  
            builder.RegisterType<BorrowerBooksManager>().As<IBorrowerBooksService>();   
        }
    }
}
