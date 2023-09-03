using LibraryProject.Domain;
using LibraryProject.DTOs;
using LibraryProject.Services;

namespace LibraryProject.Menu
{
    public class Menu
    {
        private LibraryService libraryService;

        public Menu(List<Book> books, List<BookLend> bookLends)
        {
            libraryService = new LibraryService(books, bookLends);

        }

        public void ShowChoices()
        {
            Console.WriteLine("1. Add new book to the library");
            Console.WriteLine("2. List all the library books (available and unavailable)");
            Console.WriteLine("3. Show me how many are left of this book");
            Console.WriteLine("4. Lend me a book");
            Console.WriteLine("5. Give back book");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Enter your choice: ");
        }

        public void RunMenu()
        {
            var menuIsRunning = true;
            while (menuIsRunning)
            {
                try
                {
                    ShowChoices();
                    var choice = ReadUserChoiceInput();

                    switch (choice)
                    {
                        case 1:
                            AddNewBookToLibrary();
                            break;
                        case 2:
                            ListAllTheBooks();
                            break;
                        case 3:
                            PrintRemainingBooksCount();
                            break;
                        case 4:
                            BorrowABook();
                            break;
                        case 5:
                            ReturnBook();
                            break;
                        case 6:
                            menuIsRunning = false;
                            break;
                        default:
                            {
                                break;
                            }
                    }
                    Console.WriteLine("\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Environment.Exit(0);
        }

        private void BorrowABook()
        {
            Console.WriteLine("Write the isbn of the book you want to borrow");

            var isbn = Console.ReadLine().Trim();

            var lend = libraryService.BorrowBookFromLibrary(isbn);

            Console.WriteLine($"You have borrowed {lend.Book.Title} from {lend.StartDate.Date}");
        }

        private void ReturnBook()
        {
            Console.WriteLine("If you have borrowed books for over 14 days you will pay for 1% for each extra day\n");

            var lends = libraryService.GetCurrentBookLends();

            foreach (var lend in lends)
            {
                Console.WriteLine($"{lends.IndexOf(lend)}. {lend}");
            }
            Console.WriteLine("Which book do you want to give back? (Provide the isbn)\n");

            var isbnOfBookToBeReturned = Console.ReadLine().Trim();

            var returnBookDTO = libraryService.GiveBookBackToTheLibrary(isbnOfBookToBeReturned);

            Console.WriteLine($"You are {returnBookDTO.NumberOfLateDays} Days Late so you have to pay {returnBookDTO.PriceToPay}");
        }

        private void PrintRemainingBooksCount()
        {
            Console.WriteLine("ISBN: ");
            string isbn = Console.ReadLine().Trim();

            Console.WriteLine($"There are {libraryService.CheckNumberOfBooksAvailable(isbn)} books left");
        }

        private void ListAllTheBooks()
        {
            libraryService.GetAllLibraryBooks().ForEach(book => Console.WriteLine(book));
        }

        private void AddNewBookToLibrary()
        {
            double price; string isbn; string title;

            Console.WriteLine("ISBN: ");
            isbn = Console.ReadLine().Trim();

            Console.WriteLine("Title: ");
            title = Console.ReadLine().Trim();

            Console.WriteLine("Price (Please enter a number or empty ):");
            var priceAsString = Console.ReadLine();

            priceAsString = string.IsNullOrEmpty(priceAsString.Trim()) ? "0" : priceAsString;

            if (!double.TryParse(priceAsString, out price))
            {
                throw new Exception("Please provide a number!");
            }

            libraryService.AddBookToLibrary(new BookDetailsDTO()
            {
                ISBN = isbn,
                Title = title,
                Price = price
            });
        }

        private int ReadUserChoiceInput()
        {
            int choiceNumber;
            string input = Console.ReadLine();

            while (!int.TryParse(input, out choiceNumber) || (choiceNumber <= 0 && choiceNumber > 6))
            {
                Console.WriteLine("Please enter a number between 1 and 6)");
                input = Console.ReadLine();
            }

            return choiceNumber;
        }
    }
}
