namespace DAB;

using DAB.Data;
using Microsoft.EntityFrameworkCore;

class BookReturn
{
        public static void Run()
        {
                using var context = new AppDbContext();

                /* Load all loans and their associated book */
                var loans = context.Loans.Include(l => l.Book).ToList();
                if (loans.Count < 1)
                {
                        Console.WriteLine("No books on loan");
                        return;
                }

                bool done = false;
                var input = "";
                while (!done)
                {
                        Console.WriteLine("Which book do you want to return? ");
                        input = Console.ReadLine().Trim();

                        /* Check against empty input */
                        done = string.IsNullOrEmpty(input);
                }

                loans.ForEach(l =>
                {
                        if (input == l.Book.Title)
                        {
                                /* Mark the book as available */
                                l.Book.Loan = null;

                                /* Remove the loan-entry */
                                context.Loans.Remove(l);

                                Console.WriteLine("Book was returned");
                                return;
                        }
                });
                Console.WriteLine("Book could not be returned");
        }
}