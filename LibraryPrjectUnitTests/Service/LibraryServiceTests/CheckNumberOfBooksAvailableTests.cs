using LibraryProject.ErrorMessages;
using LibraryProject.Services;

namespace LibraryUnitTests.Service.LibraryServiceTests
{
    public class CheckNumberOfBooksAvailableTests
    {
        private LibraryService _libraryService;

        public CheckNumberOfBooksAvailableTests()
        {
            InMemoryDb inMemoryDb = new InMemoryDb();
            _libraryService = new LibraryService(inMemoryDb.books, inMemoryDb.bookLends);
        }

        [Fact]
        public void CheckNumberOfBooksAvailable_Should_Throw_Exception()
        {
            //Arrange
            var invalidIsbn = new Guid().ToString();
            //Act
            Action result = () => _libraryService.CheckNumberOfBooksAvailable(invalidIsbn);
            //Assert
            Exception exception = Assert.Throws<Exception>(result);
            Assert.Equal(LibraryServiceErrorMessages.BookISBNDosentExist, exception.Message);
        }
    }
}
