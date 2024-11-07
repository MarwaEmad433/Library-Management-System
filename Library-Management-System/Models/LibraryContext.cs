using Microsoft.EntityFrameworkCore;


namespace Library_Management_System.Models
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-07CK9HF; Database=Library_MVC; Trusted_Connection=True; TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        // تعريف الجداول في قاعدة البيانات
        public DbSet<Book> Books { get; set; } // جدول الكتب
        public DbSet<Admin> Admins { get; set; } // جدول المدراء
        public DbSet<User> Users { get; set; } // جدول المستخدمين
        public DbSet<Transaction> Transactions { get; set; } // جدول المعاملات
    }
}
