using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly NoteAppDbContext _db;
		public AccountController(NoteAppDbContext context)
		{
			_db = context;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
				if (user != null)
				{
					await Authenticate(user.Email,user.Id); // аутентификация

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
				if (user == null)
				{
					// добавляем пользователя в бд
					User newUser = new User() {Email = model.Email, Password = model.Password};
					_db.Users.Add(newUser);
					await _db.SaveChangesAsync();

					await Authenticate(newUser.Email,newUser.Id); // аутентификация

					return RedirectToAction("Index", "Home");
				}
				else
					ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}

		private async Task Authenticate(string userName, int userId)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
				new Claim("Id", userId.ToString())
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}
