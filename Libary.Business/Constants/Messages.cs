using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.Constants
{
    public class Messages
    {
        //DataResult yapısı kullanarak hata kontrolü yaptığım için feedback için oluşturduğum result mesajlarım
        #region Global
        public static string success = "success";
        public static string err_null = "err_null";
        public static string unk_err = "err_unk";
        #endregion

        #region Books
        public static string book_not_found = "book_not_found";
        #endregion

        #region BorrowerBooks
        public static string borrower_book_not_found = "borrower_book_not_found";
        public static string incorrect_date = "incorrect_date";


        #endregion

    }
}
