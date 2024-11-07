using Microsoft.AspNetCore.Authentication.Cookies;

namespace Library_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add session services
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // ����� ���� ������ ������
                options.Cookie.HttpOnly = true; // ��� ������ HTTP ���
                options.Cookie.IsEssential = true; // ��� ������ �����
            });

            // Configure authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // ���� ���� ����� ������ ���������� ��������
                    options.LogoutPath = "/Account/Logout"; // ���� ����� ������
                    options.AccessDeniedPath = "/Account/AccessDenied"; // ���� ���� �����
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable session
            app.UseSession(); // ����� ��� ����� ������ �������
            app.UseAuthentication(); // ����� ��������
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
