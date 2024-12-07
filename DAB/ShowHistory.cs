namespace DAB;

using DAB.Data;
using Microsoft.EntityFrameworkCore;

class ShowHistory
{
        public static void Run()
        {
                Console.Clear();
                using var context = new AppDbContext();

                /* Load all books and their associated history */
                var books = context.Books.Include(b => b.LoanHistories).ToList();
                if (books.Count < 1)
                {
                        Console.WriteLine("Library empty");
                        return;
                }

                bool done = false;
                var input = "";
                while (!done)
                {
                        Console.WriteLine("Which book do you want to check the history of? ");
                        input = Console.ReadLine().Trim();

                        /* Check against empty input */
                        done = !string.IsNullOrEmpty(input);
                }

                books.ForEach(b =>
                {
                        if (input == b.Title)
                        {
                                b.LoanHistories.ForEach(h => Console.WriteLine($"Checked out: {h.LoanDate} and returned {h.ReturnDate}"));
                        }
                });
        }
}