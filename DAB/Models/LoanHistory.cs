namespace DAB.Models;

public class LoanHistory
{
        public int LoanHistoryID { get; set; }

        public int? BookID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
}