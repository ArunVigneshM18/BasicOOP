using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibraryManagement
{
    
    /// <summary>
    /// Enum Gender used
    /// </summary>
    public enum Gender{Select, Male, Female}
    public enum Department{Select, ECE, EEE, CSE}

    public class UserDetails
    {

        // fields
        private static int s_id = 3000;
        private string _userID;

        // properties
        public string UserID
        {
            get
            {
                return _userID;
            }
        }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public Department Department { get; set; }
        public long MobileNumber { get; set; }
        public string MailID { get; set; }
        public double WalletBalance { get; set; }

        // methods
        public void WalletRecharge (double rechargeAmount)
        {
            WalletBalance += rechargeAmount;
        }
        public void DeductBalance (double deductedAmount)
        {
            WalletBalance -= deductedAmount;
        }

        // constructor
        public UserDetails (string userName, Gender gender, Department department, long mobileNumber, string mailID, double walletBalance)
        {
            _userID = $"SF{++s_id}";
            UserName = userName;
            Gender = gender;
            Department = department;
            MobileNumber = mobileNumber;
            MailID = mailID;
            WalletBalance = walletBalance;

        }
    }
}