namespace DAB;

using DAB.Data;
using Microsoft.EntityFrameworkCore;

class ReadData
{
        public static void Run()
        {
                Console.Clear();

                using var context = new AppDbContext();

                /* We make sure to load the credits with the books and then include the author from the credits (needed due to lazy-loading) */
                /* We also include loans to read the dueDate */
                var books = context.Books
                        .Include(b => b.Credits)
                        .ThenInclude(c => c.Author)
                        .Include(b => b.Loan)
                        .ToList();

                if (books.Count < 1)
                {
                        Console.WriteLine("Library is empty");
                        return;
                }

                /* List all books and their year/author, also list if they are on loan (and if so when they will be back) or not */
                books.ForEach(b =>
                {
                        Console.Write($"Title: {b.Title}, Publishing year: {b.YearPublished} Authors: ");

                        for (int i = 0; i < b.Credits.Count; ++i)
                        {
                                Console.Write($"{b.Credits[i].Author.Name}, ");
                        }

                        Console.WriteLine("\n is {0}", b.Loan != null ? $"not available but should be back at {b.Loan.DueDate}" : "available");
                });
        }
}
