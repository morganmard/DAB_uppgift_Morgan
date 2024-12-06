namespace DAB.Models;

public class Book
{
        public int BookID { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }

        public Loan? Loan { get; set; }

        public virtual ICollection<Credit> Credits { get; set; } = [];
}