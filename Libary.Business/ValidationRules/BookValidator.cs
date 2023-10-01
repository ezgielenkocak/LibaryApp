using FluentValidation;
using LibaryApp.Entity.Concrete;
using LibaryApp.Entity.Dtos.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.ValidationRules
{
    public class BookValidator:AbstractValidator<AddBookDto>
    {
        public BookValidator()
        {
           RuleFor(x => x.BookName).NotEmpty().WithMessage("Lütfen bir kitap ismi giriniz");
           
            //RuleFor(x => x.Image).NotEmpty().WithMessage("Lütfen bir resim yükleyiniz");
            //RuleFor(x => x.Author).NotEmpty().WithMessage("Lütfen bir yazar ismi giriniz");
        }
    }
}
