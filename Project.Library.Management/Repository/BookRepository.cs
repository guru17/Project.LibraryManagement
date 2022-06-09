using Project.Library.Management.Models;
using Project.Library.Management.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;

using static Project.Library.Management.Common.StatusEnum;

namespace Project.Library.Management.Repository
{
    public class BookRepository : IBookRepository
    {
        private List<BookDetail> _bookDetails;
        private List<UserDetail> _userDetails;
        private List<BookShelf> _bookShelves;
        public BookRepository()
        {
            LoadData();
        }

        public int AddBookToShelf(int bookID, int userID)
        {
            BookShelf bookShelf = new()
            {
                RowID = _bookShelves.Max(x => x.RowID) + 1,
                BookID = bookID,
                UserID = userID,
                CreatedDate = DateTime.Now,
                Status = (int)BookReadStatus.Completed
            };
            _bookShelves.Add(bookShelf);
            return bookShelf.RowID;
        }

        public IEnumerable<BookDetail> SearchBookByName(string bookName)
        {
            return _bookDetails.Where(x => x.BookName.Contains(bookName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<BookShelfViewModel> GetCompletedBook(int? userID)
        {
            var result = _bookDetails
                        .Join(_bookShelves, b => b.BookID, bs => bs.BookID, (b, bs) => new { b, bs })
                        .Join(_userDetails, bbs => bbs.bs.UserID, u => u.UserID, (bbs, u) => new { bbs, u })
                        .Where(x => x.u.UserID == userID)
                        .Select(x => new BookShelfViewModel()
                        {
                            BookID = x.bbs.b.BookID,
                            BookName = x.bbs.b.BookName,
                            UserID = x.u.UserID,
                            UserName = x.u.UserName
                        });
            return result.Select(x => x);
        }

        public bool CheckBookExistsInShelf(int bookID, int userID)
        {
            return _bookShelves.Any(x => x.BookID == bookID && x.UserID == userID);
        }

        private void LoadData()
        {
            _bookDetails = new List<BookDetail>
            {
                new BookDetail() { BookID = 1, BookName = "Rich Dad And Poor Dad", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new BookDetail() { BookID = 2, BookName = "Into The Wild", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new BookDetail() { BookID = 3, BookName = "The Secret", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new BookDetail() { BookID = 4, BookName = "Book Of Night", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new BookDetail() { BookID = 5, BookName = "At The Quiet Edge", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new BookDetail() { BookID = 6, BookName = "The Midnight Library", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active }
            };

            _userDetails = new List<UserDetail>
            {
                new UserDetail() { UserID = 1, UserName = "User1", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new UserDetail() { UserID = 2, UserName = "User2", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active },
                new UserDetail() { UserID = 3, UserName = "User3", CreatedDate = DateTime.Now, Status = (int)RecordStatus.Active }
            };

            _bookShelves = new List<BookShelf>
            {
                new BookShelf() { RowID = 1, BookID = 1, UserID = 1, CreatedDate = DateTime.Now, Status = (int)BookReadStatus.Completed },
                new BookShelf() { RowID = 2, BookID = 2, UserID = 1, CreatedDate = DateTime.Now, Status = (int)BookReadStatus.Completed },
                new BookShelf() { RowID = 3, BookID = 3, UserID = 1, CreatedDate = DateTime.Now, Status = (int)BookReadStatus.Completed },
                new BookShelf() { RowID = 4, BookID = 1, UserID = 2, CreatedDate = DateTime.Now, Status = (int)BookReadStatus.Completed },
                new BookShelf() { RowID = 5, BookID = 2, UserID = 2, CreatedDate = DateTime.Now, Status = (int)BookReadStatus.Completed }
            };
        }

        
    }
}
