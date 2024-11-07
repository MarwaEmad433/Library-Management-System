using Library_Management_System.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private LibraryContext context;

        public AccountController()
        {
            context = new LibraryContext();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }


		[HttpPost]
		public async Task<IActionResult> UserLogin(string email, string password)
		{
			var user = context.Users.FirstOrDefault(u => u.UserEmail == email);

			if (user == null)
			{
				ModelState.AddModelError("EmailError", "Email not found.");
				return View();
			}

			if (user.UserPassword != password)
			{
				ModelState.AddModelError("PasswordError", "Incorrect password.");
				return View();
			}

			// تخزين Usernames في الجلسة
			HttpContext.Session.SetString("Usernames", user.Usernames);

			// إنشاء Claims
			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
		new Claim(ClaimTypes.Name, user.Usernames)
	};

			var claimsIdentity = new ClaimsIdentity(claims, "Login");
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			// تسجيل الدخول باستخدام Claims
			await HttpContext.SignInAsync(claimsPrincipal);

			return RedirectToAction("Index", "Home"); // تأكد من إعادة التوجيه
		}


		public IActionResult AdminLogin()
        {
            return View();
        }

		// POST: /Admin/Login
		// عند تسجيل دخول الأدمن
		[HttpPost]
		public IActionResult AdminLogin(string email, string password)
		{
			var admin = context.Admins.FirstOrDefault(a => a.AdminEmail == email);

			if (admin == null)
			{
				ModelState.AddModelError("EmailError", "Email not found.");
				return View();
			}

			if (admin.AdminPassword != password)
			{
				ModelState.AddModelError("PasswordError", "Incorrect password.");
				return View();
			}

			// تخزين AdminId و AdminName في الجلسة
			HttpContext.Session.SetInt32("AdminId", admin.AdminID);
			HttpContext.Session.SetString("AdminName", admin.AdminName);

			// إنشاء Claims
			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, admin.AdminID.ToString()),
		new Claim(ClaimTypes.Name, admin.AdminName)
	};

			var claimsIdentity = new ClaimsIdentity(claims, "AdminLogin");
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			// تسجيل الدخول باستخدام Claims
			HttpContext.SignInAsync(claimsPrincipal);

			return RedirectToAction("Index", "Home");
		}
		
			public IActionResult Logout()
			{
				HttpContext.Session.Clear(); // مسح الجلسة
				return RedirectToAction("Login");
			}


		}
}
