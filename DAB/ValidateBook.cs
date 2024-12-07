namespace DAB;
class ValidateBook
{
        public static string[] validate(out int yearPublished)
        {
                string[] output = [];
                var input = "";
                yearPublished = 0;

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

                return output;
        }
}