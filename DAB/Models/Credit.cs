namespace DAB.Models;

public class Credit
{
        public int CreditID { get; set; }

        /* required to make sure they are not empty / null */

        public int BookID { get; set; }
        public virtual required Book Book { get; set; }

        public int AuthorID { get; set; }
        public virtual required Author Author { get; set; }
}