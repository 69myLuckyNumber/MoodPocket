using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Models;
using MoodPocket.WebUI.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private IUserRepository userRepository;

		public AccountController(IUserRepository repo)
		{
			userRepository = repo;
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterModel account)
		{
			if (ModelState.IsValid)
			{
				string salt = PasswordHelperUtility.GetRandomSalt();
				string hashedPassword = PasswordHelperUtility.HashPassword(account.Password, salt);
				userRepository.CreateUser(new User()
				{
					Username = account.Username,
					Salt = salt,
					Password = hashedPassword,
					ConfirmPassword = hashedPassword,
					Email = account.Email,
					IsVerified = false,
				});
				userRepository.Complete();
				return RedirectToAction("Index", "Home");
			}
			return View(account);
		}

		[HttpPost]
		public JsonResult DoesUserExists(string Username, string Email)
		{
			User user = userRepository.Filter(Username, Email);
			return Json(user == null);
		}
		


    }
}