namespace DAB;

using DAB.Data;
using DAB.Models;
using Microsoft.EntityFrameworkCore;

class BookReturn
{
        public static void Run()
        {
                Console.Clear();
                using var context = new AppDbContext();

                /* Load all loans and their associated book (and it's history) */
                var loans = context.Loans.Include(l => l.Book).ThenInclude(b => b.LoanHistories).ToList();
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
                        done = !string.IsNullOrEmpty(input);
                }

                foreach (var loan in loans)
                {
                        if (input == loan.Book.Title)
                        {
                                /* Update the history */
                                var lh = new LoanHistory
                                {
                                        Book = loan.Book,
                                        LoanDate = loan.LoanDate,
                                        ReturnDate = DateTime.Today
                                };
                                loan.Book.LoanHistories.Add(lh);

                                /* Mark the book as available */
                                loan.Book.Loan = null;

                                /* Remove the loan-entry */
                                context.Loans.Remove(loan);

                                Console.WriteLine("Book was returned");
                                context.SaveChanges();
                                return;
                        }
                }
                Console.WriteLine("Book could not be returned");
        }
}