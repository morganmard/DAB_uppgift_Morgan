namespace DAB;

using DAB.Data;
using Microsoft.EntityFrameworkCore;

class BookReturn
{
        public static void Run()
        {
                bool done = false;
                var input = "";
                while (!done)
                {
                        Console.WriteLine("Which book do you want to return? ");
                        input = Console.ReadLine().Trim();

                        /* Check against empty input */
                        done = string.IsNullOrEmpty(input);
                }

                using var context = new AppDbContext();

                var loans = context.Loans
                        .Include(l => l.Book)
                        .ToList();

                if (loans.Count > 0)
                {
                        foreach (var loan in loans)
                        {
                                if (input == loan.Book.Title)
                                {
                                        loan.Book.Loan = null;
                                        context.Remove(loan);
                                        Console.WriteLine("Book was returned");
                                        return;
                                }
                        }
                }
                Console.WriteLine("Book could not be returned");
        }
}