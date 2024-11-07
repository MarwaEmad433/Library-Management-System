using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Library_Management_System.Models
{
    public class Transaction
    {
        [Key]
        public int TranID { get; set; } // معرف المعاملة
        public string TranStatus { get; set; } // حالة المعاملة
        public DateTime TranDate { get; set; } // تاريخ المعاملة



        [ForeignKey(nameof(Book))] // تعريف المفتاح الخارجي
        public int BookID { get; set; } // معرف الكتاب
        public string BookTitle { get; set; } // عنوان الكتاب
        public string BookISBN { get; set; } // رقم ISBN للكتاب



        [ForeignKey(nameof(User))] // تعريف المفتاح الخارجي
        public int UserID { get; set; } // معرف المستخدم
        public string Username { get; set; } // اسم المستخدم

        //Navigation proprty
        public virtual Book Book { get; set; } // التنقل إلى كلاس Book
        public virtual User User { get; set; } // التنقل إلى كلاس User
    }
}
