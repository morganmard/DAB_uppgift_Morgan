namespace DAB.Models;

public class Loan
{
        public int LoanID { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
}