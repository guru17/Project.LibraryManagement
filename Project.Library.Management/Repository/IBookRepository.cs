using Project.Library.Management.Models;
using Project.Library.Management.ViewModel;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Library.Management.Repository
{
    public interface IBookRepository
    {
        int AddBookToShelf(int bookID, int userID);
        IEnumerable<BookShelfViewModel> GetCompletedBook(int? userID);
        IEnumerable<BookDetail> SearchBookByName(string bookName);
        bool CheckBookExistsInShelf(int bookID, int userID);
    }
}
