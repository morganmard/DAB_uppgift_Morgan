namespace DAB.Models;

public class Author
{
        public int AuthorID { get; set; }

        /* required to make sure they are not empty / null */

        public required string Name { get; set; }

        public virtual required List<Credit> Credits { get; set; }
}