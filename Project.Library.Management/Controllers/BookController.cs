using Microsoft.AspNetCore.Mvc;

using Project.Library.Management.Models;
using Project.Library.Management.Repository;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Library.Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("GetCompletedBooks")]
        public IActionResult GetCompletedBooks(int? userID)
        {
            if (userID == null || userID <= 0)
            {
                return BadRequest("Invalid UserID");
            }
            try
            {
                var result = _bookRepository.GetCompletedBook(userID);
                if (!result.Any())
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("SearchBookByName")]
        public IActionResult SearchBookByName(string bookName = null)
        {
            if (bookName == null)
            {
                return BadRequest("Invalid Input");
            }
            try
            {
                IEnumerable<BookDetail> result = _bookRepository.SearchBookByName(bookName);
                if (!result.Any())
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddBookToShelf")]
        public IActionResult AddBookToShelf(int bookID = 0, int userID = 0)
        {
            try
            {
                //Validation check
                if (bookID <= 0)
                {
                    return BadRequest("Invalid BookID");
                }
                if (userID <= 0)
                {
                    return BadRequest("Invalid UserID");
                }
                if (_bookRepository.CheckBookExistsInShelf(bookID, userID))
                {
                    return BadRequest("Already Added");
                }
                var result = _bookRepository.AddBookToShelf(bookID, userID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
