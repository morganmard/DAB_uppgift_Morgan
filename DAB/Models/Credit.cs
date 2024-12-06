namespace DAB.Models;

public class Credit
{
        public int CreditID { get; set; }

        public int BookID { get; set; }
        public virtual Book Book { get; set; }

        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
}