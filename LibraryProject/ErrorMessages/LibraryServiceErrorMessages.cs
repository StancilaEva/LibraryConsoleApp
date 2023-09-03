using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ErrorMessages
{
    public static class LibraryServiceErrorMessages
    {
        public const string BookISBNDosentExist = "No books with this isbn";
        public const string NoBookWithThisISBNHasBeenLendToYou = "No borrowed books with this isbn";
        public const string BookIsntAvailable = "There are 0 available books from this title. Maybe try another book";

    }
}
