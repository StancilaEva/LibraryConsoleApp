using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUnitTests.DomainTests.BookLendTests
{
    public class GetNumberOfLateDaysTests
    {
        private BookLend bookLend;

        [Theory]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(-14)]
        public void GetNumberOfLateDaysTests_Should_Return_0(int daysFromStartDate)
        {
            //Arrange
            var isbn = "isbn 1";
            bookLend = new BookLend(
                isbn,
                new Book(isbn, "Some Title", 3.4),
                DateTime.Now.AddDays(daysFromStartDate)
            );

            //Act
            var result = bookLend.GetNumberOfLatedays();

            //Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(-15)]
        public void GetNumberOfLateDaysTests_Should_Return_1_Day_Late(int daysFromStartDate)
        {
            //Arrange
            var isbn = "isbn 1";
            bookLend = new BookLend(
                isbn,
                new Book(isbn, "Some Title", 3.4),
                DateTime.Now.AddDays(daysFromStartDate)
            );

            //Act
            var result = bookLend.GetNumberOfLatedays();

            //Assert
            Assert.Equal(1, result);
        }
    }
}
