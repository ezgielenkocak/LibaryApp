using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Business.Constants
{
    public class Messages
    {
        public static string success = "success";
        public static string err_null = "err_null";
        public static string unk_err = "err_unk";

        #region Books
        public static string book_not_found = "book_not_found";
        #endregion

        #region BorrowerBooks
        public static string borrower_book_not_found = "borrower_book_not_found";

        #endregion

    }
}
