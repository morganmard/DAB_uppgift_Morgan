namespace DAB;

using DAB.Models;
using DAB.Data;
using Microsoft.EntityFrameworkCore;

class Checkout
{

        public static void Run()
        {
                bool done = false;
                var input = "";
                while (!done)
                {
                        Console.WriteLine("Which book do you want to chekout?");
                        input = Console.ReadLine().Trim(); ;

                        /* Check against empty input */
                        done = string.IsNullOrEmpty(input);
                }

                using var context = new AppDbContext();

                /* Load all books and loans */
                var books = context.Books
                                .Include(b => b.Loan)
                                .ToList();

                if (books.Count > 0)
                {
                        foreach (var book in books)
                        {
                                if (book.Title == input)
                                {
                                        /* Make sure the book is available */
                                        if (book.Loan == null)
                                        {
                                                var loan = new Loan
                                                {
                                                        Book = book,
                                                        LoanDate = DateTime.Today,
                                                        DueDate = DateTime.Today.AddMonths(1)
                                                };

                                                context.Loans.Add(loan);

                                                /* Mark the book as loaned out */
                                                book.Loan = loan;

                                                context.SaveChanges();

                                                Console.WriteLine("Book checked out sucessfully");
                                                return;
                                        }
                                        break;
                                }
                        }
                }
                Console.WriteLine("Could not checkout book");
        }
}
