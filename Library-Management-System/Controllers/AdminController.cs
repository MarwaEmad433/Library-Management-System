using Azure.Core;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_Management_System.Controllers
{
    public class AdminController : Controller
    {
        private LibraryContext context;

        public AdminController()
        {
            context = new();
        }

        // عرض صفحة الكتب
        public IActionResult Books()
        {
            List<Book> books = context.Books.ToList(); // جلب جميع الكتب من قاعدة البيانات
            return View(books);
        }

        // عرض صفحة إضافة الكتاب
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        // استقبال بيانات الكتاب من الفورم وحفظها في قاعدة البيانات
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            book.BookDateAdded = DateTime.Now; // تعيين تاريخ الإضافة
            context.Books.Add(book);
            context.SaveChanges();
            TempData["SuccessMessage"] = "Book has been added successfully."; // عرض رسالة نجاح
            return RedirectToAction("Books"); // إعادة التوجيه إلى صفحة الكتب
        }

        // عرض صفحة تحرير الكتاب
        [HttpGet]
        public IActionResult EditBook(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return NotFound(); // إذا لم يتم العثور على الكتاب
            }
            return View(book);
        }

        // تحديث الكتاب
        [HttpPost]
        public IActionResult EditBook(int id, Book updatedBook)
        {
            Book? oldBook = context.Books.SingleOrDefault(b => b.BookID == id);
            if (oldBook == null) return View("NotFound"); // إذا لم يتم العثور على الكتاب

            // تحديث بيانات الكتاب
            oldBook.BookTitle = updatedBook.BookTitle;
            oldBook.BookCategory = updatedBook.BookCategory;
            oldBook.BookAuthor = updatedBook.BookAuthor;
            oldBook.BookCopies = updatedBook.BookCopies;
            oldBook.BookISBN = updatedBook.BookISBN;

            // الاحتفاظ بالتاريخ الأصلي إذا لم يتم تغييره
            if (updatedBook.BookDateAdded != DateTime.MinValue)
            {
                oldBook.BookDateAdded = updatedBook.BookDateAdded;
            }

            oldBook.BookStatus = updatedBook.BookStatus;

            // تحديث الكتاب في قاعدة البيانات
            context.Books.Update(oldBook);
            context.SaveChanges();

            TempData["SuccessMessage"] = "Book has been updated successfully."; // عرض رسالة نجاح
            return RedirectToAction("Books"); // إعادة التوجيه إلى صفحة الكتب
        }

        // عرض تفاصيل الكتاب
        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return NotFound(); // إذا لم يتم العثور على الكتاب
            }
            return View(book); // تمرير الكتاب إلى صفحة التفاصيل
        }

        // حذف الكتاب
        [HttpDelete]
        public IActionResult Delete(int bookId)
        {
            var book = context.Books.Find(bookId);
            if (book == null)
            {
                return Json(new { success = false });
            }

            context.Books.Remove(book);
            context.SaveChanges();

            return Json(new { success = true });
        }

        // GET: /Admin/Users
        public IActionResult Users()
        {
            List<User> users = context.Users.ToList(); // الحصول على جميع المستخدمين
            return View(users); // عرض المستخدمين في العرض
        }

        // عرض صفحة إضافة مستخدم
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        // استقبال بيانات المستخدم من الفورم وحفظها في قاعدة البيانات
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            
                context.Users.Add(user);
                context.SaveChanges();
                TempData["SuccessMessage"] = "User has been added successfully."; // عرض رسالة نجاح
                return RedirectToAction("Users"); // إعادة التوجيه إلى صفحة المستخدمين
           
        }

        // عرض صفحة تحرير مستخدم
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound(); // إذا لم يتم العثور على المستخدم
            }
            return View(user);
        }

        // تحديث المستخدم
        [HttpPost]
        public IActionResult EditUser(int id, User updatedUser)
        {
            User? oldUser = context.Users.SingleOrDefault(u => u.UserID == id);
            if (oldUser == null) return View("NotFound"); // إذا لم يتم العثور على المستخدم

            // تحديث بيانات المستخدم
            oldUser.Usernames = updatedUser.Usernames;
            oldUser.UserGender = updatedUser.UserGender;
            oldUser.UserDep = updatedUser.UserDep;
            oldUser.UserAdmNo = updatedUser.UserAdmNo;
            oldUser.UserEmail = updatedUser.UserEmail;

            // تحديث كلمة المرور فقط إذا كانت جديدة
            if (!string.IsNullOrWhiteSpace(updatedUser.UserPassword))
            {
                oldUser.UserPassword = updatedUser.UserPassword; // تحديث كلمة المرور الجديدة
            }

            // تحديث المستخدم في قاعدة البيانات
            context.Users.Update(oldUser);
            context.SaveChanges();

            TempData["SuccessMessage"] = "User has been updated successfully."; // عرض رسالة نجاح
            return RedirectToAction("Users"); // إعادة التوجيه إلى صفحة المستخدمين
        }



        // عرض تفاصيل المستخدم
        [HttpGet]
        public IActionResult UserDetails(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound(); // إذا لم يتم العثور على المستخدم
            }
            return View(user); // تمرير المستخدم إلى صفحة التفاصيل
        }

        // حذف المستخدم
        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            context.Users.Remove(user);
            context.SaveChanges();

            return Json(new { success = true });
        }

        // عرض جميع الطلبات المعلقة
        public ActionResult Requests()
        {
            // جلب جميع الطلبات المعلقة مع الكتب والمستخدمين
            var requests = context.Transactions
                .Include(t => t.Book)   // تأكد من أنك تستخدم Include بشكل صحيح
                .Include(t => t.User)   // تأكد من أنك تستخدم Include بشكل صحيح
                .Where(t => t.TranStatus == "Requested")
                .ToList(); // تنفيذ الاستعلام واسترجاع البيانات

            return View(requests); // إرسال البيانات إلى العرض
        }


        [HttpPost]
        public JsonResult AcceptRequest(int id)
        {
            // العثور على المعاملة باستخدام المعرف
            var transaction = context.Transactions.FirstOrDefault(t => t.TranID == id);
            if (transaction == null)
            {
                return Json(new { success = false, message = "Transaction not found." });
            }

            // العثور على الكتاب باستخدام BookISBN
            var book = context.Books.FirstOrDefault(b => b.BookISBN == transaction.BookISBN);

            if (book != null)
            {
                // التحقق من وجود نسخ متاحة
                if (book.BookCopies > 0)
                {
                    // تغيير الحالة إلى "Received" بعد قبول الطلب
                    transaction.TranStatus = "Received";
                    transaction.TranDate = DateTime.Now;

                    // تقليل عدد النسخ المتاحة
                    book.BookCopies -= 1;

                    // حفظ التغييرات في قاعدة البيانات
                    context.SaveChanges();

                    // إعادة رسالة نجاح
                    return Json(new { success = true, message = "Request accepted successfully and book status updated." });
                }
                else
                {
                    // إذا لم تكن هناك نسخ متاحة
                    return Json(new { success = false, message = "No copies available." });
                }
            }
            else
            {
                // إعادة رسالة خطأ في حال عدم العثور على الكتاب
                return Json(new { success = false, message = "Book not found." });
            }
        }


        [HttpPost]
        public ActionResult RejectRequest(int id)
        {
            // الحصول على الطلب باستخدام المعرف
            var transaction = context.Transactions.Find(id);
            if (transaction != null)
            {
                // تحديث حالة الطلب إلى "مرفوض"
                transaction.TranStatus = "Rejected";
                context.SaveChanges();

                // إعادة رسالة نجاح
                return Json(new { success = true, message = "Request rejected successfully." });
            }

            // إعادة رسالة خطأ في حال عدم العثور على الطلب
            return Json(new { success = false, message = "Request not found." });
        }



        public ActionResult Accepted()
        {
            // جلب جميع الطلبات المعلقة مع الكتب والمستخدمين
            // جلب الطلبات المقبولة فقط
            var acceptedRequests = context.Transactions
                .Where(t => t.TranStatus == "Received") // أو "issued" حسب الحالة
                .ToList();

            return View(acceptedRequests);
        }

        // عرض جميع طلبات الإرجاع
        public ActionResult ReturnedRequests()
        {
            // جلب جميع الطلبات مع حالة الإرجاع
            var returnedRequests = context.Transactions
                .Include(t => t.Book)  // جلب معلومات الكتاب
                .Include(t => t.User)   // جلب معلومات المستخدم
                .Where(t => t.TranStatus == "Returned") // تحديد حالة الإرجاع
                .ToList(); // تنفيذ الاستعلام واسترجاع البيانات

            return View(returnedRequests); // إرسال البيانات إلى العرض
        }
       
        [HttpPost]
        public JsonResult AcceptReturn(int id)
        {
            // Find the transaction based on the ID
            var transaction = context.Transactions.FirstOrDefault(t => t.TranID == id);

            if (transaction != null)
            {
                // Find the book based on BookISBN
                var book = context.Books.FirstOrDefault(b => b.BookISBN == transaction.BookISBN);

                if (book != null)
                {
                    // Increase the available copies of the book
                    book.BookCopies += 1;

                    // Change the return status of the transaction
                    transaction.TranStatus = "Accepted";
                    transaction.TranDate = DateTime.Now;
                    context.SaveChanges(); // Save changes to the database

                    // Return a success message
                    return Json(new { success = true, message = "Return accepted and book stock updated." });
                }
                else
                {
                    // Return error if the book is not found
                    return Json(new { success = false, message = "Book not found." });
                }
            }

            // Return error if the transaction is not found
            return Json(new { success = false, message = "Transaction not found." });
        }


    }
}
