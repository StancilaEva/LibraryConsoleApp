using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ErrorMessages
{
    public static class BookErrorMessages
    {
        public const string InvalidISBN = "Invalid ISBN";
        public const string InvalidTitle = "Title must be at least a character long";
        public const string InvalidPrice = "Price must be bigger than 0";
    }
}
