namespace DAB.Models;

public class Book
{
        public int BookID { get; set; }

        /* required to make sure they are not empty / null */

        public required string Title { get; set; } = "";
        public required int YearPublished { get; set; }

        /* 'Loan?' means it can be null */
        public Loan? Loan { get; set; }

        public virtual required List<Credit> Credits { get; set; }
}