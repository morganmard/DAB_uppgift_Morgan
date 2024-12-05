namespace DAB;

using DAB.Models;
using DAB.Data;

class AddData
{

        public static void Run()
        {
                Console.Clear();
                const string m = "\nAdd a book by specifying 'Title, Year, Author1, Author2, etc' (Q to go back)";

                var input = "";
                string[]? output = [];
                var errorString = "";
                int year = 0;

                while (input is not ("Q" or "q"))
                {
                        /* Print possible parsing-error from pervious loop-iteration (prints nothing if it's the first iteration) */
                        Console.Write(errorString);

                        /* Our standard (and for the time only) error message */
                        errorString = "Format Error";

                        /* Print the menu/help */
                        Console.WriteLine(m);


                        input = Console.ReadLine();
                        output = input.Split(',');

                        /* Check the length of the splitted-input */
                        if (output.Length < 3 || output.Length > 20) continue;


                        /* Validate the input */
                        var validTitle = output[0].Trim() is not "";
                        var validYear = int.TryParse(output[1], out year);


                        /* Validate the authors (rest of the input) */
                        var validAuthors = true;

                        for (int i = 2; i < output.Length; ++i)
                        {
                                if (output[i].Trim() == "")
                                {
                                        validAuthors = false;
                                }
                        }

                        /* Print errro and try again if any input is invalid */
                        if (!validAuthors || !validTitle || !validYear) continue;

                        break;
                }

                /* At this point all the input is validate, the title is stored in output[0], the year in 'year' and the author(s) in output index 2 and higher */


                using var context = new AppDbContext();

                context.Database.EnsureCreated();

                var transaction = context.Database.BeginTransaction();
                try
                {
                        var book = new Book
                        {
                                Title = output[0].Trim(),
                                YearPublished = year
                        };

                        var author = new Author
                        {
                                Name = output[2].Trim()

                        };

                        var Credit = new Credit // fix
                        {
                                Book = book,
                                Author = author
                        };

                        context.Books.Add(book);
                        context.Authors.Add(author);
                        context.Credits.Add(Credit);

                        context.SaveChanges();
                        transaction.Commit();
                }
                catch (Exception ex)
                {
                        transaction.Rollback();
                        Console.WriteLine(ex);
                }
        }
}