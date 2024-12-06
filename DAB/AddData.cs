namespace DAB;

using DAB.Models;
using DAB.Data;

class AddData
{

        public static void Run()
        {
                Console.Clear();

                /* Our string-array to save Title and authors */
                string[]? output = [];
                int yearPublished = 0;


                var input = "";
                while (input is not ("Q" or "q"))
                {
                        /* Print the menu/help */
                        Console.WriteLine("\nAdd a book by specifying 'Title, Year, Author1, Author2, etc' (Q to go back)");

                        input = Console.ReadLine();

                        /* Split the input into segments */
                        output = input.Split(',');

                        /* Check the length of the splitted-input to make sure it has atleast all the 3 mandatory fields (leaving up to 17 extra authors) */
                        if (output.Length < 3 || output.Length > 20) continue;


                        /* Validate the input */

                        /* Checks for empty title*/
                        var validTitle = output[0].Trim() is not "";

                        /* Check to make sure the year is a valid integer (saving the output in 'yearPublished') */
                        var validYear = int.TryParse(output[1], out yearPublished);


                        /* Validate the authors (rest of the input) */
                        var validAuthors = true;

                        for (int i = 2; i < output.Length; ++i)
                        {
                                if (output[i].Trim() == "")
                                {
                                        validAuthors = false;
                                }
                        }

                        if (!validAuthors || !validTitle || !validYear) continue;

                        /* Break out of the loop if all the input is sucessfully validated */
                        break;
                }

                /* At this point the title is stored in output[0], the year in 'yearPublished' and the author(s) in output index 2 and higher */

                using var context = new AppDbContext();

                /* Start a transaction */
                var transaction = context.Database.BeginTransaction();
                try
                {
                        var book = new Book
                        {
                                Title = output[0].Trim(),
                                YearPublished = yearPublished,
                                Loan = null
                        };
                        context.Books.Add(book);

                        /* Extract rest of the authors and add them (if they don't already exists */
                        for (int i = 0; i < output.Length; ++i)
                        {
                                var author = new Author
                                {
                                        Name = output[i + 2].Trim()

                                };
                                context.Authors.Add(author); // is unique

                                var Credit = new Credit
                                {
                                        Book = book,
                                        Author = author
                                };
                                context.Credits.Add(Credit);

                                /* After creating a 'bridge' (our credit) we add it to the book and author to make a relation */
                                book.Credits.Add(Credit);
                                author.Credits.Add(Credit);
                        }

                        context.SaveChanges();
                        transaction.Commit();

                        Console.WriteLine("Book added");
                }
                catch (Exception ex)
                {
                        transaction.Rollback();
                        Console.WriteLine(ex);
                }
        }
}