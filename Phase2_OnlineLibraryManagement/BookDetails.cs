using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibraryManagement
{
    public class BookDetails
    {
        // fields
        private static int s_id = 100;
        private string _bookID;

        // properties
        public string BookID
        {
            get
            {
                return _bookID;
            }
        }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int BookCount { get; set; }

        public BookDetails (string bookName, string authorName, int bookCount)
        {
            _bookID = $"BID{++s_id}";
            BookName = bookName;
            AuthorName = authorName;
            BookCount = bookCount;
        }
    }
}