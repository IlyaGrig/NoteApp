using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.BusinessLogicLayer.Interfaces;

namespace NoteApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAuthService _authServise;
		public AccountController(IAuthService authServise)
		{
			_authServise = authServise;
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
			if (!ModelState.IsValid) return View(model);
			if ((_authServise.UserCheck(model)).Result)
			{
				var user = (_authServise.GetUser(model)).Result;
				await Authenticate(user.Email, user.Id); // аутентификация

				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", "Некорректные логин и(или) пароль");
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
			if (!ModelState.IsValid) return View(model);
			if (!(_authServise.UserCheck(model)).Result)
			{
				// добавляем пользователя в бд

				await _authServise.SetUser(model);
				var user = _authServise.GetUser(model).Result;
				await Authenticate(user.Email,user.Id); // аутентификация

				return RedirectToAction("Index", "Home");
			}
			else
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
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
			var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
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
