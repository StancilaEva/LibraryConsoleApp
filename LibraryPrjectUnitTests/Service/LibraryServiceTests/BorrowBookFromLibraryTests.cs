using LibraryProject.Domain;
using LibraryProject.DTOs;
using LibraryProject.ErrorMessages;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUnitTests.Service.LibraryServiceTests
{
    public class BorrowBookFromLibraryTests
    {
        private LibraryService _libraryService;

        [Fact]
        public void BorrowBookFromLibrary_Should_Throw_Unavailable_Book_Exception()
        {
            //Arrange
            InMemoryDb inMemoryDb = new InMemoryDb();

            var unavailableBook = new Book(Guid.NewGuid().ToString(), "Test", 0, 2.3);
            inMemoryDb.books.Add(unavailableBook);

            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);

            //Act
            Action result = () => _libraryService.BorrowBookFromLibrary(unavailableBook.ISBN);

            //Assert
            Exception exception = Assert.Throws<Exception>(result);
            Assert.Equal(LibraryServiceErrorMessages.BookIsntAvailable, exception.Message);
        }

        [Fact]
        public void BorrowBookFromLibrary_Should_Throw_Invalid_ISBN_Exception()
        {
            //Arrange
            InMemoryDb inMemoryDb = new InMemoryDb();
            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);

            var invalidIsbn = new Guid().ToString();
            //Act
            Action result = () => _libraryService.BorrowBookFromLibrary(invalidIsbn);
            //Assert
            Exception exception = Assert.Throws<Exception>(result);
            Assert.Equal(LibraryServiceErrorMessages.BookISBNDosentExist, exception.Message);
        }
    }
}
