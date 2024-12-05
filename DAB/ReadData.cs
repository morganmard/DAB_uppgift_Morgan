namespace DAB;

using DAB.Data;

class ReadData
{
        public static void Run()
        {
                Console.Clear();

                using var context = new AppDbContext();

                var books = context.Books
                    .ToList();

                if (books.Count > 0)
                {
                        foreach (var book in books)
                        {
                                Console.WriteLine($"Title: {book.Title}");
                        }
                }
        }
}
