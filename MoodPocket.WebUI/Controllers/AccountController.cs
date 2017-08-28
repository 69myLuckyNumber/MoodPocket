using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Models;
using MoodPocket.WebUI.Utilities;
using MoodPocket.WebUI.Filters;

using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace MoodPocket.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private IUnitOfWork unitOfWork;

		public AccountController(IUnitOfWork uow)
		{
			unitOfWork = uow;
		}

		[HttpGet]
		[OnlyAnonymous]
		[Route("entry")]
		public ViewResult Entry()
		{
			RegLogViewModel model = new RegLogViewModel()
			{
				Register = new RegisterModel(),
				Login = new LoginModel()
			};
			return View(model);
		}

		[HttpPost]
		[AjaxValidate]
		[OnlyAnonymous]
		[ValidateAntiForgeryToken]
		[Route("Account/Register")]
		public JsonResult Register(RegisterModel account)
		{
			if (ModelState.IsValid)
			{
				string salt = PasswordHelperUtility.GetRandomSalt();
				string hashedPassword = PasswordHelperUtility.HashPassword(account.Password, salt);
				unitOfWork.UserRepository.CreateUser(new User()
				{
					Username = account.Username,
					Salt = salt,
					Password = hashedPassword,
					ConfirmPassword = hashedPassword,
					Email = account.Email,
					IsVerified = false,
					Gallery = null
				});
				unitOfWork.Commit();
                return new JsonResult() { Data = "Signed-up"};
			}
			return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DoesUserExists(string Username, string Email)
		{
			User user = unitOfWork.UserRepository.Filter(Username, Email);
			return Json(user == null);
		}

		[HttpPost]
		[AjaxValidate]
		[OnlyAnonymous]
		[ValidateAntiForgeryToken]
		[Route("Account/Login")]
		public JsonResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = unitOfWork.UserRepository.Filter(model.Username);
				if(user != null)
				{
					if(PasswordHelperUtility.ValidatePassword(model.Password, user.Password, user.Salt))
					{
						FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        return new JsonResult() { Data = "Logged-in" };
					}
				}
			}
			HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
			ModelState.AddModelError("Username", "Invalid credentials");
			var errors = from field in ModelState.Keys
						 where ModelState[field].Errors.Count > 0
						 select new
						 {
							 key = field,
							 errors = ModelState[field].Errors
											.Select(f => f.ErrorMessage)
											.ToArray()
						 };

			return new JsonResult() { Data = errors };
		}

		[Authorize]
		[Route("Account/LogOut")]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Entry");
		}

	}
}