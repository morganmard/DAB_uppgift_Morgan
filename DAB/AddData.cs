namespace DAB;

using DAB.Models;
using DAB.Data;

class AddData
{

        /* Guard against duplicates */ /* Ugly hack, but don't know any better way */
        /* Checks list of authors and looks for dupes, if a dupe is found we use the old author, otherwise create a new one */
        public static Author CheckDupe(List<Author> list, out bool dupe, string newName)
        {
                Author ret = null;
                dupe = false;

                foreach (var oldauthor in list)
                {
                        if (oldauthor.Name == newName)
                        {
                                dupe = true;
                                ret = oldauthor;
                                break;
                        }
                }

                ret ??= new Author
                {
                        Name = newName,
                        Credits = []

                };

                return ret;
        }

        public static void Run()
        {
                Console.Clear();

                /* Our string-array to save Title and authors and our year variable being set in validate*/
                var output = ValidateBook.validate(out int yearPublished, out bool quit);

                if (quit)
                {
                        return;
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
                                Loan = null,
                                Credits = [],
                                LoanHistories = []
                        };

                        context.Books.Add(book); // is unique (Will fail for duplicate titles)

                        /* Extract rest of the authors and add them (if they don't already exists) */
                        for (int i = 2; i < output.Length; ++i)
                        {

                                /* Guard against duplicates */ /* Slow and ugly, but I don't know any better way */
                                var author = CheckDupe(context.Authors.ToList(), out bool dupe, output[i].Trim());
                                if (!dupe)
                                {
                                        context.Authors.Add(author);
                                }

                                /* Create a credit for each Book <-> author pair */
                                var credit = new Credit
                                {
                                        Book = book,
                                        Author = author
                                };

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