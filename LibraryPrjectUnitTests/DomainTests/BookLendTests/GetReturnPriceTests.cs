
using LibraryProject.Domain;
using Xunit;

namespace LibraryTests.DomainTests.BookLendTests
{
    public class GetReturnPriceTests
    {
        private BookLend bookLend;

        [Theory]
        [InlineData(-14)]
        [InlineData(-7)]
        [InlineData(0)]
        public void GetReturnPrice_Should_Return_Full_Price_Without_Penalty(int daysSinceLend)
        {
            //Arrange
            var isbn = "isbn 1";
            bookLend = new BookLend(
                isbn,
                new Book(isbn, "Some Title", 3.4),
                DateTime.Now.AddDays(daysSinceLend)
            );

            //Act
            var result = bookLend.GetReturnPrice();

            //Assert
            Assert.Equal(bookLend.Book.Price,result);
        }


        [Theory]
        [InlineData(-20)]
        [InlineData(-15)]
        public void GetReturnPrice_Should_Return_Full_Price_With_Penalty(int daysSinceLend)
        {
            //Arrange
            var isbn = "isbn 1";
            bookLend = new BookLend(
                isbn,
                new Book(isbn, "Some Title", 3.4),
                DateTime.Now.AddDays(daysSinceLend)
            );

            //Act
            var result = bookLend.GetReturnPrice();

            //Assert
            var expectedPrice = bookLend.Book.Price * 0.01 * ((DateTime.Now - bookLend.StartDate).Days - 14) + bookLend.Book.Price;
            Assert.Equal(expectedPrice, result);
        }
    }
}
