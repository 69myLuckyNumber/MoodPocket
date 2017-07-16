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
				userRepository.CreateUserAndSave(new User()
				{
					Username = account.Username,
					Salt = salt,
					Password = hashedPassword,
					ConfirmPassword = hashedPassword,
					Email = account.Email,
				});
				return RedirectToAction("Index", "Home");
			}
			return View(account);
		}
	

    }
}