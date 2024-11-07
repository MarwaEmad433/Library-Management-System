using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext context;

        public BookController()
        {
            context = new();
        }

        public IActionResult BookList()
        {
            var books = context.Books.ToList();
            return View(books);
        }

        public IActionResult RequestBook(int bookId)
        {
            try
            {
                var userIdCookie = HttpContext.Request.Cookies["UserId"];
                if (string.IsNullOrEmpty(userIdCookie))
                {
                    return Json(new { success = false, message = "User not authenticated." });
                }

                int userId = int.Parse(userIdCookie);
                var book = context.Books.Find(bookId);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found." });
                }

                if (book.BookCopies <= 0)
                {
                    return Json(new { success = false, message = "No copies available for this book." });
                }

                // استرجاع اسم المستخدم من قاعدة البيانات بناءً على UserID
                var user = context.Users.Find(userId);
                if (user == null || string.IsNullOrEmpty(user.Usernames))
                {
                    return Json(new { success = false, message = "Username not found." });
                }

                var transaction = new Transaction
                {
                    BookID = bookId,
                    UserID = userId,
                    TranDate = DateTime.Now,
                    TranStatus = "Requested",
                    BookTitle = book.BookTitle,
                    BookISBN = book.BookISBN,
                    Username = user.Usernames // تعيين اسم المستخدم هنا
                };

                context.Transactions.Add(transaction);
                context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Your request has been successfully sent!",
                    Title = transaction.Book?.BookTitle,
                    Author = transaction.Book?.BookAuthor,
                    RequestDate = transaction.TranDate.ToShortDateString()
                });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "An error occurred while processing your request: " + errorMessage });
            }
        }



        public IActionResult RequestedBooks()
        {
            var userIdCookie = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdCookie);

            var requests = context.Transactions
                .Include(t => t.Book)
                .Where(t => t.UserID == userId && (t.TranStatus == "Requested" || t.TranStatus == "Re-requested")) // تصفية الحالات
                .Select(t => new
                {
                    RequestId = t.TranID,
                    BookTitle = t.Book.BookTitle,
                    Author = t.Book.BookAuthor,
                    RequestDate = t.TranDate
                })
                .ToList();

            return View(requests);
        }


        [HttpPost]
        public IActionResult Cancel(int requestId)
        {
            var transaction = context.Transactions.Find(requestId);
            if (transaction != null)
            {
                context.Transactions.Remove(transaction);
                context.SaveChanges();
            }

            return RedirectToAction("RequestedBooks");
        }

        public IActionResult ReceivedBooks()
        {
            var receivedBooks = context.Transactions
                .Include(t => t.Book)
                .Include(t => t.User)
                .Where(t => t.TranStatus == "Received")
                .ToList();

            return View(receivedBooks);
        }

        public IActionResult ReturnBook(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.TranID == id);

            if (transaction != null)
            {
                transaction.TranStatus = "Returned";
                transaction.TranDate = DateTime.Now;
                context.SaveChanges();

                TempData["SuccessMessage"] = "Book returned successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Book not found.";
            }

            return Json(new { success = true, message = TempData["SuccessMessage"] });
        }

        public IActionResult RejectedBooks()
        {
            var rejectedRequests = context.Transactions
                .Where(t => t.TranStatus == "Rejected")
                .Include(t => t.Book)
                .Select(t => new
                {
                    RequestId = t.TranID,
                    Title = t.Book.BookTitle,
                    Author = t.Book.BookAuthor,
                    RequestDate = t.TranDate,
                })
                .ToList();

            return View(rejectedRequests);
        }

        [HttpPost]
        public IActionResult ReRequest(int requestId)
        {
            var transaction = context.Transactions
                .Include(t => t.Book)
                .FirstOrDefault(t => t.TranID == requestId && t.TranStatus == "Rejected");

            if (transaction != null)
            {
                transaction.TranStatus = "Requested";
                context.SaveChanges();
            }

            return RedirectToAction("RejectedBooks");
        }

        [HttpPost]
        public IActionResult CancelRequest(int requestId)
        {
            var transaction = context.Transactions
                .FirstOrDefault(t => t.TranID == requestId && t.TranStatus == "Rejected");

            if (transaction != null)
            {
                transaction.TranStatus = "Cancelled";
                context.SaveChanges();
            }

            return RedirectToAction("RejectedBooks");
        }
    }
}
