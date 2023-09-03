using LibraryProject.Domain;
using LibraryProject.DTOs;
using LibraryProject.ErrorMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Services
{
    public class LibraryService
    {
        private List<Book> Books;
        private List<BookLend> BookLends;
        public LibraryService(List<Book> books, List<BookLend> bookLends)
        {
            this.Books = books;
            this.BookLends = bookLends;
        }

        public List<Book> GetAllLibraryBooks()
        {
            return Books;
        }

        public Book AddBookToLibrary(BookDetailsDTO bookDetailsDTO)
        {
            var existingBook = Books.FirstOrDefault(book => book.ISBN == bookDetailsDTO.ISBN);

            if (existingBook != null)
            {
                existingBook.IncreaseBookQuantity();

                return existingBook;
            }
            else
            {
                var newBook = new Book(bookDetailsDTO.ISBN, bookDetailsDTO.Title, 1, bookDetailsDTO.Price);
                Books.Add(newBook);

                return newBook;
            }
        }

        public int CheckNumberOfBooksAvailable(string isbn)
        {
            var book = Books.FirstOrDefault(book => book.ISBN.Equals(isbn));

            return book is null
                ? throw new Exception(LibraryServiceErrorMessages.BookISBNDosentExist)
                : book.Quantity - BookLends.Where(lend => lend.BookISBN.Equals(isbn)).Count();
        }

        public ReturnedBookDTO GiveBookBackToTheLibrary(string isbn)
        {
            // Get all the lends with this type of book and order them by date so that we can get oldest borrowed book first
            var lend = BookLends.Where(bookLends => bookLends.BookISBN.Equals(isbn)).OrderBy(booklends => booklends.StartDate).FirstOrDefault();

            if (lend is null)
                throw new Exception(LibraryServiceErrorMessages.NoBookWithThisISBNHasBeenLendToYou);

            BookLends.Remove(lend);

            return new ReturnedBookDTO()
            {
                NumberOfLateDays = lend.GetNumberOfLatedays(),
                PriceToPay = lend.GetReturnPrice()
            };
        }

        public BookLend BorrowBookFromLibrary(string isbn)
        {
            var bookToBeBorrowed = Books.FirstOrDefault(book => book.ISBN.Equals(isbn));

            IsBookLendValid(isbn, bookToBeBorrowed);

            var bookLend = new BookLend(isbn, bookToBeBorrowed, DateTime.Now);

            BookLends.Add(bookLend);

            return bookLend;
        }

        private void IsBookLendValid(string isbn, Book? bookToBeBorrowed)
        {
            if (bookToBeBorrowed is null)
                throw new Exception(LibraryServiceErrorMessages.BookISBNDosentExist);

            if (bookToBeBorrowed.Quantity - BookLends.Where(lend => lend.Book.ISBN.Equals(isbn)).Count() == 0)
                throw new Exception(LibraryServiceErrorMessages.BookIsntAvailable);
        }

        public List<BookLend> GetCurrentBookLends()
        {
            return BookLends;
        }
    }
}
