using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLibraryManagement;

public enum Menu{Select, Registration, Login, Exit}
public enum SubMenu{Select, BorrowBook, BorrowedHistory, ReturnBooks, WalletRecharge, WalletBalance, Exit}
class Program 
{
    
    // Default Data List
    public static List<UserDetails> UserList = new List<UserDetails>();
    public static List<BookDetails> BookList = new List<BookDetails>();
    public static List<BorrowDetails> BorrowList = new List<BorrowDetails>();

    static UserDetails LoggedInUser;


    public static void Main(string [] args)
    {

        AddDefaultData();

        Console.WriteLine("\nWelcome to Synfcusion College Library Portal!");

        bool run = true;

        // main loop
        while (run)
        {

            // main menu
            Console.WriteLine("\nPlease choose any one from the below options.\n1.Registration\n2.Login\n3.Exit\nEither enter the number or enter the name as shown above.");

            bool menuCheck;
            Menu chosenMenu;
            
            menuCheck = Enum.TryParse<Menu>(Console.ReadLine(), true, out chosenMenu);

            if (menuCheck)
            {
                switch (chosenMenu)
                {
                    case Menu.Registration:
                        Registration();
                        break;

                    case Menu.Login:
                        Login();
                        break;

                    case Menu.Exit:
                        run = false;
                        break;

                    default:
                        menuCheck = false;
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }

    // add default data method
    public static void AddDefaultData()
    {

        UserList.Add(new UserDetails("Ravichandran", Gender.Male, Department.EEE, 9938388333, "ravi@gamil.com", 100));
        UserList.Add(new UserDetails("Priyadharshini", Gender.Female, Department.CSE, 9944444455, "priya@gamil.com", 150));

        BookList.Add(new BookDetails("C#", "Author1", 3));
        BookList.Add(new BookDetails("HTML", "Author2", 5));
        BookList.Add(new BookDetails("CSS", "Author1", 3));
        BookList.Add(new BookDetails("JS", "Author1", 3));
        BookList.Add(new BookDetails("TS", "Author2", 2));
        
        BorrowList.Add(new BorrowDetails("BID101", "SF3001", new DateTime(2023,09,10), Status.Borrowed, 2, 0));
        BorrowList.Add(new BorrowDetails("BID103", "SF3001", new DateTime(2023,09,12), Status.Borrowed, 1, 0));
        BorrowList.Add(new BorrowDetails("BID104", "SF3001", new DateTime(2023,09,14), Status.Returned, 1, 16));
        BorrowList.Add(new BorrowDetails("BID102", "SF3002", new DateTime(2023,09,11), Status.Borrowed, 2, 0));
        BorrowList.Add(new BorrowDetails("BID105", "SF3002", new DateTime(2023,09,09), Status.Returned, 1, 20));

    }

    // registration
    public static void Registration()
    {
        Console.WriteLine("\nFill out the below form");

        // username
        bool userNameCheck = false;
        string userName;

        do
        {
            Console.Write("\nUser Name: ");
            userName = Console.ReadLine();

            if (userName.Any(char.IsDigit)) Console.WriteLine("Name should not contain any numbers.");
            else userNameCheck = true;
        } while (!userNameCheck);

        // gender
        bool genderCheck = false;
        Gender gender;

        do
        {
            Console.WriteLine("\nSelect the gender\n1.Male\n2.Female\nEither enter the number or enter the name as shown above.");
            Console.Write("Gender: ");
            genderCheck = Enum.TryParse<Gender>(Console.ReadLine(), true, out gender);

            if ((!Enum.IsDefined(typeof(Gender), gender)) || (gender == Gender.Select)) genderCheck = false;

            if (!genderCheck) Console.WriteLine("Invalid gender. Please try again.");

        } while (!genderCheck);

        // department
        bool departmentCheck = false;
        Department department;

        do
        {
            Console.WriteLine("\nSelect the department\n1.ECE\n2.EEE\n3.CSE\nEither enter the number or enter the name as shown above.");
            Console.Write("Department: ");
            departmentCheck = Enum.TryParse<Department>(Console.ReadLine(), true, out department);

            if ((!Enum.IsDefined(typeof(Department), department)) || (department == Department.Select)) departmentCheck = false;

            if (!departmentCheck) Console.WriteLine("Invalid option. Please try again.");
        } while (!departmentCheck);

        // mobile number
        bool mobileCheck = false;
        long mobileNumber;

        do
        {
            Console.Write("\nPhone: (+91) ");
            mobileCheck = long.TryParse(Console.ReadLine(), out mobileNumber);

            if (!mobileCheck) Console.WriteLine("Invalid number. Please try again.");
            else if (!(mobileNumber.ToString().Length == 10))
            {
                mobileCheck = false;
                Console.WriteLine("Please enter 10 digits correctly.");
            }
        } while (!mobileCheck);

        // mail ID
        bool mailIDCheck = false;
        string mailID;
        
        do
        {
            Console.Write("\nMail ID: ");
            mailID = Console.ReadLine();

            if (mailID.Contains('@') && mailID.EndsWith(".com")) mailIDCheck = true;
            else Console.WriteLine("Invalid Mail ID. Please try again.");
        } while (!mailIDCheck);

        // balance
        bool balanceCheck = false;
        double walletBalance;

        do
        {
            Console.Write("\nWallet Balance: (Rs.) ");
            balanceCheck = double.TryParse(Console.ReadLine(), out walletBalance);

            if (!balanceCheck) Console.WriteLine("Invalid amount. Please try again.");
            else if (walletBalance < 0)
            {
                balanceCheck = false;
                Console.WriteLine("The amount can't be in negative value. Enter a valid amount.");
            }
        } while (!balanceCheck);

        // add the registered user data to an object and add it to the users list
        UserDetails user = new UserDetails(userName, gender, department, mobileNumber, mailID, walletBalance);
        UserList.Add(user);

        Console.WriteLine($"\nKindly check your details\nName: {user.UserName}\nGender: {user.Gender}\nDepartment: {user.Department}\nPhone: {user.MobileNumber}\nMail ID: {user.MailID}\nWallet Balance: {Math.Round(user.WalletBalance,2)}\n\nPlease make note of your User ID\nUser ID - {user.UserID}");
    }

    // login method
    public static void Login()
    {
        Console.WriteLine("\nEnter your User ID: ");
        
        string userID = Console.ReadLine();
        bool userCheck = false;

        foreach (UserDetails user in UserList)
        {
            if (user.UserID == userID.ToUpper())
            {
                LoggedInUser = user;
                SubMenu subMenu;

                userCheck = true;
                bool subMenuCheck = false;

                do
                {
                    // sub menu
                    Console.WriteLine("\nPlease choose any one from the below options.\n1.Borrow Book\n2.Borrowed History\n3.Return Books\n4.Wallet Recharge\n5.Wallet Balance\n6.Exit\nEither enter the number or enter the name as shown above without any space.");

                    subMenuCheck = Enum.TryParse<SubMenu>(Console.ReadLine(), true, out subMenu);

                    if (!subMenuCheck)
                    {
                        subMenuCheck = true;
                        
                        Console.WriteLine("Invalid option. Please try again");
                    }

                    else
                    {
                        switch(subMenu)
                        {
                            case SubMenu.BorrowBook:
                                BorrowBook();
                                break;

                            case SubMenu.BorrowedHistory:
                                BorrowedHistory();
                                break;

                            case SubMenu.ReturnBooks:
                                ReturnBooks();
                                break;

                            case SubMenu.WalletRecharge:
                                WalletRecharge();
                                break;

                            case SubMenu.WalletBalance:
                                Console.WriteLine($"\nYour wallet Balance is Rs.{LoggedInUser.WalletBalance}");
                                break;

                            case SubMenu.Exit:
                                subMenuCheck = false;
                                break;

                            default:
                                Console.WriteLine("Invalid input. Please try again.");
                                break;
                        }
                    }
                } while (subMenuCheck);
            }
        }
        if (!userCheck) Console.WriteLine("Invalid User ID. Please enter a valid one.");
    }

    // borrow method
    public static void BorrowBook()
    {
        Console.WriteLine($"\nHere are the list of books available for borrowing");

        Console.WriteLine("\nBookID".PadRight(20) + "Book Name".PadRight(20) + "Author Name".PadRight(20) + "Book Count".PadRight(20));
        
        foreach (BookDetails book in BookList)
        {
            Console.WriteLine($"{book.BookID.PadRight(20)}|{book.BookName.PadRight(20)}|{book.AuthorName.PadRight(20)}|{book.BookCount.ToString()}");
        }

        Console.WriteLine("\nChoose a book by entering Book ID");
        
        bool bookCheck = false;
        
        string bookID;

        do
        {
            Console.Write("Book ID: ");
            
            bookID = Console.ReadLine();

            foreach (BookDetails book in BookList)
            {
                if (book.BookID == bookID.ToUpper())
                {
                    bookCheck = true;

                    bool countCheck;
                    
                    int count;

                    do
                    {
                        Console.Write("Count (Enter '0' to cancel): ");
                        countCheck = int.TryParse(Console.ReadLine(), out count);

                        if (!countCheck) Console.WriteLine("Invalid Count. Please try again.");

                        else if (count>0)
                        {
                            if (count > book.BookCount)
                            {
                                Console.WriteLine($"We don't have the required count of books. The book count available is {book.BookCount}");
                                
                                DateTime previousBorrowedDate;

                                foreach (BorrowDetails borrow in BorrowList)
                                {
                                    if (borrow.BookID == book.BookID)
                                    {
                                        previousBorrowedDate = borrow.BorrowDate;

                                        Console.WriteLine($"The book will be available from {previousBorrowedDate.AddDays(15).ToString("dd/MM/yyyy")}");
                                    }
                                }
                            }

                            else
                            {
                                int userBookCount = 0;

                                foreach (BorrowDetails borrow in BorrowList)
                                {
                                    if ((borrow.UserID == LoggedInUser.UserID) && (borrow.Status == Status.Borrowed))
                                    {
                                        userBookCount+=borrow.BorrowBookCount;
                                    }
                                }

                                if (userBookCount+count>=4)
                                {
                                    Console.WriteLine($"You have borrowed {userBookCount} books already");
                                }

                                else
                                {
                                    book.BookCount -= count;

                                    BorrowDetails borrow = new BorrowDetails(book.BookID, LoggedInUser.UserID, DateTime.Today, Status.Borrowed, count, 0);

                                    BorrowList.Add(borrow);

                                    Console.WriteLine($"You have successfully borrowed");
                                }
                            }
                        }
                    } while (!countCheck);

                    break;
                }
            }
            if (!bookCheck) Console.WriteLine("Invalid Book ID.");

        } while (!bookCheck);      
    }

    // borrowed history method
    public static void BorrowedHistory()
    {
        Console.WriteLine("\nBorrowID".PadRight(20) + "Book ID".PadRight(20) + "User ID".PadRight(20) + "Borrow Date".PadRight(20) + "Status".PadRight(20) + "Count".PadRight(20) + "Paid Fine Amount".PadRight(20));
        
        bool bookPresentCheck = false;

        foreach (BorrowDetails borrow in BorrowList)
        {
            if (borrow.UserID == LoggedInUser.UserID)
            {
                bookPresentCheck = true;

                Console.WriteLine($"{borrow.BorrowID.PadRight(20)}{borrow.BookID.PadRight(20)}{borrow.UserID.PadRight(20)}{borrow.BorrowDate.ToString("dd/MM/yyyy").PadRight(20)}{borrow.Status.ToString().PadRight(20)}{borrow.BorrowBookCount.ToString().PadRight(20)}{borrow.PaidFineAmount.ToString()}");
            }
        }

        if (!bookPresentCheck) Console.WriteLine("\nYou have not borrowed any books till now.");
    }

    // wallet recharge method
    public static void WalletRecharge()
    {
        Console.WriteLine("\nDo you wish to recharge your wallet Balance?\nEnter Yes/No.");

        string response = Console.ReadLine();

        if (response.ToLower() == "yes")
        {
            bool amountCheck = false;

            double amount;

            do
            {
                Console.Write("\nAmount: ");

                amountCheck = double.TryParse(Console.ReadLine(), out amount);

                if (!amountCheck) Console.WriteLine("Invalid amount.");
                
                else if (amount<0)
                {
                    amountCheck = false;

                    Console.WriteLine("Invalid amount.");
                }

                else if (amount>0)
                {
                    LoggedInUser.WalletRecharge(amount);

                    Console.WriteLine($"Your balance is {Math.Round(LoggedInUser.WalletBalance,2)}");
                }
            } while (!amountCheck);
        }

        else
        {
            Console.WriteLine("\nSwitching back to Sub Menu");
        }
    }

    // return books method
    public static void ReturnBooks()
    {

        Console.WriteLine("Here are the books borrowed by you:");

        Console.WriteLine("\nBorrowID".PadRight(20) + "Book ID".PadRight(20) + "User ID".PadRight(20) + "Borrow Date".PadRight(20) + "Status".PadRight(20) + "Count".PadRight(20) + "Paid Fine Amount".PadRight(20) + "Return Date");
        
        foreach (BorrowDetails borrow in BorrowList)
        {
            if ((borrow.UserID==LoggedInUser.UserID) && (borrow.Status==Status.Borrowed))
            {
                Console.WriteLine($"{borrow.BorrowID.PadRight(20)}{borrow.BookID.PadRight(20)}{borrow.UserID.PadRight(20)}{borrow.BorrowDate.ToString("dd/MM/yyyy").PadRight(20)}{borrow.Status.ToString().PadRight(20)}{borrow.BorrowBookCount.ToString().PadRight(20)}{borrow.PaidFineAmount.ToString().PadRight(20)}{borrow.BorrowDate.AddDays(15).ToString("dd/MM/yyyy")}");
            }
        }

        bool returnBookIDCheck = false;
        
        do
        {
            Console.WriteLine("\nEnter the Borrow ID to return. (Enter 0 to go back to sub menu.)");
            
            string returnBorrowID = Console.ReadLine();

            if (returnBorrowID=="0") returnBookIDCheck = true;

            else
            {
                foreach (BorrowDetails borrow in BorrowList)
                {
                    if ((borrow.UserID==LoggedInUser.UserID) && (borrow.Status==Status.Borrowed) && (borrow.BorrowID==returnBorrowID.ToUpper()))
                    {
                        returnBookIDCheck = true;
                        
                        double fineAmount = 0;

                        TimeSpan daysElapsed = DateTime.Today - borrow.BorrowDate;

                        if (daysElapsed.TotalDays > 15)
                        {
                            fineAmount = daysElapsed.TotalDays * borrow.BorrowBookCount;

                            if (LoggedInUser.WalletBalance<fineAmount)
                            {
                                Console.WriteLine($"Your fine amount is {fineAmount} & you have insufficient balance. Please recharge before returning the book");
                            }

                            else
                            {
                                LoggedInUser.DeductBalance(fineAmount);
                                
                                borrow.Status = Status.Returned;
                                
                                borrow.PaidFineAmount = fineAmount;
                                
                                Console.WriteLine("Book Returned Successfully");
                            }
                        }

                        else
                        {
                            borrow.Status = Status.Returned;
                            
                            Console.WriteLine("Book Returned Successfully");
                        }

                        break;
                    }
                }
            }
            
            if (!returnBookIDCheck) Console.WriteLine("Invalid Book ID. Please try again.");
        
        } while (!returnBookIDCheck);
    }
}