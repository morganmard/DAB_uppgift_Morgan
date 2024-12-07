namespace DAB.Models;

public class LoanHistory
{
        public int LoanHistoryID { get; set; }

        /* required to make sure they are not empty / null */

        public int BookID { get; set; }
        public required Book Book { get; set; }

        public required DateTime LoanDate { get; set; }
        public required DateTime ReturnDate { get; set; }
}