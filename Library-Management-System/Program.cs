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
                options.IdleTimeout = TimeSpan.FromMinutes(30); //  ÕœÌœ › —… «‰ Â«¡ «·Ã·”…
                options.Cookie.HttpOnly = true; // Ã⁄· «·ﬂÊﬂÌ HTTP ›ﬁÿ
                options.Cookie.IsEssential = true; // Ã⁄· «·ﬂÊﬂÌ √”«”Ì
            });

            // Configure authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // „”«— ’›Õ…  ”ÃÌ· «·œŒÊ· ··„” Œœ„Ì‰ «·⁄«œÌÌ‰
                    options.LogoutPath = "/Account/Logout"; // „”«—  ”ÃÌ· «·Œ—ÊÃ
                    options.AccessDeniedPath = "/Account/AccessDenied"; // „”«— ’›Õ… «·—›÷
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
            app.UseSession(); // ≈÷«›… Â–Â «·”ÿ— · „ﬂÌ‰ «·Ã·”« 
            app.UseAuthentication(); //  „ﬂÌ‰ «·„’«œﬁ…
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
