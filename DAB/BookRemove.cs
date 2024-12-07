using DAB.Data;
using DAB.Models;
using Microsoft.EntityFrameworkCore;

class BookRemove
{
        public static void Run()
        {

                Console.Clear();
                using var context = new AppDbContext();

                /* Load all books with credits and loan */
                var books = context.Books.Include(b => b.Credits).Include(b => b.Loan).ToList();

                if (books.Count < 1)
                {
                        Console.WriteLine("Library is empty");
                        return;
                }

                bool done = false;
                var input = "";
                while (!done)
                {
                        Console.WriteLine("Which book do you want to remove?");
                        input = Console.ReadLine().Trim(); ;

                        /* Check against empty input */
                        done = !string.IsNullOrEmpty(input);
                }

                books.ForEach(b =>
                {
                        if (input == b.Title)
                        {
                                /* Make sure to remove the credit to the book */
                                b.Credits.ForEach(c => context.Credits.Remove(c));

                                /* If it's on loan, remove the loan entry */
                                if (b.Loan != null)
                                {
                                        context.Loans.Remove(b.Loan);
                                }
                                context.Books.Remove(b);
                                Console.WriteLine("Book removed sucessfully");
                                context.SaveChanges();
                                return;
                        }
                });
                Console.WriteLine("No book removed");
        }
}