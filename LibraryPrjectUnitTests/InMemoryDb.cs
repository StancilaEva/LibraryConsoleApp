using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUnitTests
{
    public class InMemoryDb
    {
        public List<Book> books;
        public List<BookLend> bookLends;

        public InMemoryDb()
        {
            books = new List<Book>()
            {
              new Book("9780804172707","A Little Life",2,1.2),
              new Book("9781250787316","Sharks in time of saviors",1,4),
              new Book("9780307744432","Night Circus",3,2.1),
              new Book("9780063070738", "Anansi Boys",1,2)
             };

            bookLends = new List<BookLend>()
            {
               new BookLend(books[0].ISBN, books[0], DateTime.Now.AddDays(-16)),
               new BookLend(books[1].ISBN, books[1],DateTime.Now.AddDays(-8)),
               new BookLend(books[3].ISBN, books[3], DateTime.Now.AddDays(-3))
            };
        }
    }
}
