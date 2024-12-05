namespace DAB.Models;

public class Book
{
        public int BookID { get; set; }
        public string? Title { get; set; }
        public int YearPublished { get; set; }

        public Loan? Loan { get; set; }

        public ICollection<Credit> Credits { get; } = [];
}