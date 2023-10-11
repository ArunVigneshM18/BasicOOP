using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibraryManagement
{

    // enum
    public enum Status{Default, Borrowed, Returned}

    public class BorrowDetails
    {
        // fields
        private static int s_id = 300;
        private string _borrowID;

        // properties
        public string BorrowID
        {
            get
            {
                return _borrowID;
            }
        }
        public string BookID { get; set; }
        public string UserID { get; set; }
        public DateTime BorrowDate { get; set; }
        public Status Status { get; set; }
        public int BorrowBookCount { get; set; }
        public double PaidFineAmount { get; set; }

        // constructor
        public BorrowDetails(string bookID, string userID, DateTime borrowDate, Status status, int borrowBookCount, double paidFineAmount)
        {
            _borrowID = $"LB{++s_id}";
            BookID = bookID;
            UserID = userID;
            BorrowDate = borrowDate;
            Status = status;
            BorrowBookCount = borrowBookCount;
            PaidFineAmount = paidFineAmount;
        }
    }
}