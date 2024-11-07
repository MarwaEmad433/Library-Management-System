using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } // معرف المستخدم
        public string Usernames { get; set; } // اسم المستخدم
        public string UserGender { get; set; } // جنس المستخدم
        public string UserDep { get; set; } // قسم المستخدم
        public string UserAdmNo { get; set; } // رقم تسجيل المستخدم
        public string UserEmail { get; set; } // بريد المستخدم الإلكتروني
        public string UserPassword { get; set; } // كلمة مرور المستخدم
        
        // Navigation property
        public virtual ICollection<Transaction> Transactions { get; set; } // معاملات المستخدم
    }
}

