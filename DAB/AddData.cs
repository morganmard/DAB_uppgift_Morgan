namespace DAB;

using DAB.Models;
using DAB.Data;

class AddData
{

        public static void Run()
        {
                Console.Clear();

                /* Our string-array to save Title and authors and our year variable being set in validate*/
                var output = ValidateBook.validate(out int yearPublished);

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
                                Loan = null,
                                Credits = []
                        };

                        context.Books.Add(book); // is unique (only adds the book if not already in the table)

                        /* Extract rest of the authors and add them (if they don't already exists) */
                        for (int i = 2; i < output.Length; ++i)
                        {
                                var author = new Author
                                {
                                        Name = output[i].Trim(),
                                        Credits = []

                                };

                                /* Create a credit for each Book <-> author pair */
                                var credit = new Credit
                                {
                                        Book = book,
                                        Author = author
                                };

                                context.Authors.Add(author); // is unique

                                /* Add the creadit to the database */
                                context.Credits.Add(credit);

                                /* Make sure the add the connection between book and author by using the 'bridge'-table credit */
                                author.Credits.Add(credit);
                                book.Credits.Add(credit);

                        }

                        /* Save changes and try to commit the transaction */
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