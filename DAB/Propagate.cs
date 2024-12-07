namespace DAB;

using DAB.Data;
using DAB.Models;
using Microsoft.EntityFrameworkCore;

class Propagate
{
        public static void Run()
        {
                using var context = new AppDbContext();

                /* Make sure the database is up and running (and create it if not) */
                context.Database.EnsureCreated();

                var books = context.Books.ToList();

                /* If the database is already filled with entries, return */
                if (books.Count > 0) return;

                /* Populate the databaste with 3 books and checkout one book */

                string[] Titles = ["Design Patterns", "The Art of Computer Programming", "Computers and Typesetting"];
                int[] Years = [1994, 1968, 1984];
                string[] Authors = ["Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides", "Donald Knuth", "Donald Knuth"];

                for (int i = 0; i < Titles.Length; ++i)
                {
                        var book = new Book
                        {
                                Title = Titles[i],
                                YearPublished = Years[i],
                                Loan = null,
                                Credits = [],
                                LoanHistories = []
                        };
                        context.Books.Add(book);

                        Authors[i].Split(',').ToList().ForEach(a =>
                        {

                                /* Make sure to save the current context before checking for duplicates */
                                context.SaveChanges();

                                var author = AddData.CheckDupe(context.Authors.ToList(), out bool dupe, a);
                                if (!dupe)
                                {
                                        context.Authors.Add(author);
                                }


                                var credit = new Credit
                                {
                                        Book = book,
                                        Author = author
                                };
                                context.Credits.Add(credit);

                                author.Credits.Add(credit);
                                book.Credits.Add(credit);
                        });
                }

                context.SaveChanges();

                var bookWithLoan = context.Books.Include(b => b.Loan).First();

                var loan = new Loan
                {
                        Book = bookWithLoan,
                        LoanDate = DateTime.Today,
                        DueDate = DateTime.Today.AddMonths(1)
                };
                context.Loans.Add(loan);

                bookWithLoan.Loan = loan;
                context.SaveChanges();
        }
}