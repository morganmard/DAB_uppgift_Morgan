namespace DAB;

using DAB.Data;
using Microsoft.EntityFrameworkCore;

class ReadData
{
        public static void Run()
        {
                Console.Clear();

                using var context = new AppDbContext();

                var books = context.Books
                        .Include(b => b.Credits)
                        .ToList();

                if (books.Count > 0)
                {
                        foreach (var book in books)
                        {
                                Console.Write($"Title: {book.Title} Publishing year: {book.YearPublished}");
                                var authors = book.Credits.Select(c => c.Author).ToList();
                                foreach (var author in authors)
                                {
                                        Console.Write(author.Name);
                                }
                                Console.WriteLine("");
                        }
                }
        }
}
