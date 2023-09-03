using LibraryProject.ErrorMessages;
using System.Text.RegularExpressions;

namespace LibraryProject.Domain
{
    public class Book
    {
        public string ISBN { get; }
        public string Title { get; }
        public double Price { get; }
        public int Quantity { get; private set; }

        public Book(string iSBN, string title, int quantity, double price)
        {
            IsBookValid(iSBN, title, price);

            ISBN = iSBN;
            Title = title;
            Quantity = quantity;
            Price = price;
        }

        public Book(string iSBN, string title, double price)
        {
            IsBookValid(iSBN, title, price);

            ISBN = iSBN;
            Title = title;
            Price = price;
            Quantity = 1;
        }

        private static void IsBookValid(string iSBN, string title, double price)
        {
            var regex = new Regex("^(?=(?:\\D*\\d){10}(?:(?:\\D*\\d){3})?$)[\\d-]+$");
            if (string.IsNullOrEmpty(iSBN) || !regex.IsMatch(iSBN))
            {
                throw new Exception(BookErrorMessages.InvalidISBN);
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new Exception(BookErrorMessages.InvalidTitle);
            }

            if (price <= 0)
            {
                throw new Exception(BookErrorMessages.InvalidPrice);
            }
        }

        public void IncreaseBookQuantity()
        {
            Quantity++;
        }

        public override string? ToString()
        {
            return $"ISBN: {ISBN},\nTitle: {Title},\nQuantity: {Quantity},\nPrice: {Price}";
        }
    }
}
