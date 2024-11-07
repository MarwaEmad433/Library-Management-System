using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; } // معرف المدير
        public string AdminName { get; set; } // اسم المدير
        public string AdminEmail { get; set; } // بريد المدير الإلكتروني
        public string AdminPassword { get; set; } // كلمة مرور المدير
    }
}
