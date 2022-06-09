using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Project.Library.Management.Controllers;
using Project.Library.Management.Models;
using Project.Library.Management.Repository;
using Project.Library.Management.ViewModel;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Project.LibraryManagement.UnitTest
{
    public class BookUnitTestController
    {
        private readonly IBookRepository bookRespository;
        public BookUnitTestController()
        {
            bookRespository = new BookRepository();
        }
        
        #region GetBookByName
        [Fact]
        public void Task_SearchBookByName_Return_OkResult()
        {
            var controller = new BookController(bookRespository);
            string bookName = "Into The Wild";

            var result = controller.SearchBookByName(bookName);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Task_SearchBookByName_Return_NotFound()
        {
            var controller = new BookController(bookRespository);
            string bookName = "Dream Town";

            var result = controller.SearchBookByName(bookName);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Task_SearchBookByName_Return_MatchingResults()
        {
            var controller = new BookController(bookRespository);
            string bookName = "the";

            var result = controller.SearchBookByName(bookName);

            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var books = okResult.Value.Should()
                .BeAssignableTo<IEnumerable<BookDetail>>().Subject
                .ToList();

            Assert.Equal("Into The Wild", books[0].BookName);
            Assert.Equal("The Secret", books[1].BookName);
            Assert.Equal("At The Quiet Edge", books[2].BookName);
            Assert.Equal("The Midnight Library", books[3].BookName);
        }

        [Fact]
        public void Task_SearchBookByName_Return_BadRequest()
        {
            var controller = new BookController(bookRespository);

            var result = controller.SearchBookByName(null);

            Assert.IsType<BadRequestObjectResult>(result);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            Assert.Equal("Invalid Input", badRequestResult.Value);
        }
        #endregion

        #region GetCompletedBook
        [Fact]
        public void Task_GetCompletedBook_Return_OkResult()
        {
            var controller = new BookController(bookRespository);
            int userID = 1;

            var result = controller.GetCompletedBooks(userID);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Task_GetCompletedBook_Return_NotFound()
        {
            var controller = new BookController(bookRespository);
            int userID = 3;

            var result = controller.GetCompletedBooks(userID);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Task_GetCompletedBook_Return_BadRequest()
        {
            var controller = new BookController(bookRespository);

            var result = controller.GetCompletedBooks(null);

            Assert.IsType<BadRequestObjectResult>(result);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            Assert.Equal("Invalid UserID", badRequestResult.Value);
        }

        [Fact]
        public void Task_GetCompletedBook_Return_MatchingResult()
        {
            var controller = new BookController(bookRespository);
            int userID = 2;

            var result = controller.GetCompletedBooks(userID);

            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var books = okResult.Value.Should().BeAssignableTo<IEnumerable<BookShelfViewModel>>().Subject.ToList();

            Assert.Equal(userID, books[0].UserID);
            Assert.Equal("Rich Dad And Poor Dad", books[0].BookName);

            Assert.Equal(userID, books[1].UserID);
            Assert.Equal("Into The Wild", books[1].BookName);
        }
        #endregion

        #region AddBooksToShelf
        [Fact]
        public void Task_AddBookToShelf_Return_OkResult()
        {
            var controller = new BookController(bookRespository);
            int bookID = 1;
            int userID = 5;

            var result = controller.AddBookToShelf(bookID, userID);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void Task_AddBookToShelf_Invalid_UserID_Return_BadRequest()
        {
            var controller = new BookController(bookRespository);
            int bookID = 1;
            int userID = 0;

            var result = controller.AddBookToShelf(bookID, userID);

            Assert.IsType<BadRequestObjectResult>(result);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            Assert.Equal("Invalid UserID", badRequestResult.Value);
        }

        [Fact]
        public void Task_AddBookToShelf_Invalid_BookID_Return_BadRequest()
        {
            var controller = new BookController(bookRespository);
            int bookID = 0;
            int userID = 1;

            var result = controller.AddBookToShelf(bookID, userID);

            Assert.IsType<BadRequestObjectResult>(result);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            Assert.Equal("Invalid BookID", badRequestResult.Value);
        }

        [Fact]
        public void Task_AddBookToShelf_Book_Already_Exists_Return_BadRequest()
        {
            var controller = new BookController(bookRespository);
            int bookID = 1;
            int userID = 1; 
            var result = controller.AddBookToShelf(bookID, userID);
            Assert.IsType<BadRequestObjectResult>(result);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            Assert.Equal("Already Added", badRequestResult.Value);
        }

        [Fact]
        public void Task_AddBookToShelf_Return_LastInsertedID_OkResult()
        {
            var controller = new BookController(bookRespository);
            int bookID = 6;
            int userID = 5;
            int expectedId = 6;
            var result = controller.AddBookToShelf(bookID, userID);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            Assert.Equal(expectedId, okResult.Value);
        }
        #endregion
    }
}
