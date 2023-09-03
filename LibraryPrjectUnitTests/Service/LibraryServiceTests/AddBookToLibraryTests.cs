using LibraryProject.Domain;
using LibraryProject.DTOs;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUnitTests.Service.LibraryServiceTests
{
    public class AddBookToLibraryTests
    {
        private LibraryService _libraryService;

        [Fact]
        public void AddBookToLibrary_Should_Increase_Book_Quantity()
        {
            //Arrange
            InMemoryDb inMemoryDb = new InMemoryDb();
            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);

            var bookToIncrease = inMemoryDb.books[0];
            var oldQuantity = bookToIncrease.Quantity;

            //Act
            _libraryService.AddBookToLibrary(new BookDetailsDTO()
            {
                ISBN = bookToIncrease.ISBN
            });

            //Assert
            Assert.Equal(oldQuantity + 1, bookToIncrease.Quantity);
        }

        [Fact]
        public void AddBookToLibrary_Should_Create_A_New_Book()
        {
            //Arrange
            InMemoryDb inMemoryDb = new InMemoryDb();
            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);

            var isbn = Guid.NewGuid().ToString();
            var newBook = new BookDetailsDTO()
            {
                ISBN = isbn,
                Price = 1,
                Title = "Test",
            };

            //Act
            var addedBook = _libraryService.AddBookToLibrary(newBook);

            //Assert
            Assert.Single(inMemoryDb.books, addedBook);
            Assert.Equal(isbn, addedBook.ISBN);
        }
    }
}
