namespace DAB.Models;

public class Credit
{
        public int CreditID { get; set; }

        public int BookID { get; set; }
        public Book? Book { get; set; }

        public int AuthorID { get; set; }
        public Author? Author { get; set; }
}