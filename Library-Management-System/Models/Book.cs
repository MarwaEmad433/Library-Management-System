using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; } // معرف الكتاب
        public string BookTitle { get; set; } // عنوان الكتاب
        public string BookCategory { get; set; } // فئة الكتاب
        public string BookAuthor { get; set; } // مؤلف الكتاب
        public int BookCopies { get; set; } // عدد النسخ المتاحة
        public string BookISBN { get; set; } // رقم ISBN للكتاب
        public DateTime BookDateAdded { get; set; } // تاريخ إضافة الكتاب
        public string BookStatus { get; set; } // حالة الكتاب

        // Navigation property
        public virtual ICollection<Transaction> Transactions { get; set; } // معاملات الكتاب
    }
}
