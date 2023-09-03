
namespace LibraryProject.Domain
{
    public class BookLend
    {
        public BookLend(string bookISBN, Book book, DateTime startDate)
        {
            BookISBN = bookISBN;
            Book = book;
            StartDate = startDate;
        }
        public string BookISBN { get; }
        public Book Book { get; }
        public DateTime StartDate { get; }

        public override string? ToString()
        {
            return $"{Book} \nHas been borrowed for {(DateTime.Now - StartDate).Days} Days";
        }

        public double GetReturnPrice()
        {
            if ((DateTime.Now - StartDate).Days <= 14) return Book.Price;
            else
                return ((DateTime.Now - StartDate).Days - 14) * 0.01 * Book.Price + Book.Price;
        }

        public int GetNumberOfLatedays()
        {
            if ((DateTime.Now - StartDate).Days <= 14) return 0;
            else
                return (DateTime.Now - StartDate).Days - 14;
        }
    }
}
