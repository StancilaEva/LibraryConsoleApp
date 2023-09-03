
using LibraryProject.Domain;
using LibraryProject.ErrorMessages;
using LibraryProject.Services;

namespace LibraryUnitTests.Service.LibraryServiceTests
{
    public class GiveBookBackToTheLibraryTests
    {
        private LibraryService _libraryService;

        [Fact]
        public void GiveBookBackToTheLibraryTests_Should_Throw_No_Book_Borrowed_With_This_ISBN_Exception()
        {
            //Arrange
            InMemoryDb inMemoryDb = new InMemoryDb();
            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);

            var invalidIsbn = new Guid().ToString();
            //Act
            Action result = () => _libraryService.GiveBookBackToTheLibrary(invalidIsbn);
            //Assert
            Exception exception = Assert.Throws<Exception>(result);
            Assert.Equal(LibraryServiceErrorMessages.NoBookWithThisISBNHasBeenLendToYou, exception.Message);
        }

        [Fact]
        public void GiveBookBackToTheLibraryTests_Should_Remove_Lend_From_The_List()
        {
            //Arrange
            InMemoryDb inMemoryDb = new InMemoryDb();

            var lend = inMemoryDb.bookLends[0];

            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);

            //Act
            var result = _libraryService.GiveBookBackToTheLibrary(lend.Book.ISBN);

            //Assert
            Assert.Null(inMemoryDb.bookLends.FirstOrDefault(booklend => booklend.Equals(lend)));
        }
    }
}
