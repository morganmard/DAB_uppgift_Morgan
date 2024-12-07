namespace DAB;

using DAB.Models;
using DAB.Data;
using Microsoft.EntityFrameworkCore;

class Checkout
{

        public static void Run()
        {
                using var context = new AppDbContext();

                /* Load all books and loans */
                var books = context.Books.Include(b => b.Loan).ToList();

                if (books.Count < 1)
                {
                        Console.WriteLine("Library is empty");
                        return;
                }

                bool done = false;
                var input = "";
                while (!done)
                {
                        Console.WriteLine("Which book do you want to chekout?");
                        input = Console.ReadLine().Trim(); ;

                        /* Check against empty input */
                        done = string.IsNullOrEmpty(input);
                }

                books.ForEach(b =>
                {
                        if (b.Title == input)
                        {
                                /* Check if the book is available */
                                if (b.Loan == null)
                                {
                                        var loan = new Loan
                                        {
                                                Book = b,
                                                LoanDate = DateTime.Today,
                                                DueDate = DateTime.Today.AddMonths(1)
                                        };
                                        context.Loans.Add(loan);
                                        /* Mark the book as unavilable for next customer */
                                        b.Loan = loan;

                                        context.SaveChanges();
                                        Console.WriteLine("Book checked out sucessfully");

                                        /* Early return to skip the error message below */
                                        return;
                                }

                        }
                });

                Console.WriteLine("Could not checkout book");
        }
}
}
