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
using MoodPocket.WebUI.Utilities.Abstract;
using System;

namespace MoodPocket.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private IUnitOfWork unitOfWork;
        private IStringHasher stringHashService;
        private IEmailSender emailSendService;
		public AccountController(IUnitOfWork uow, IStringHasher strHasher, IEmailSender emailSender)
		{
			unitOfWork = uow;
            stringHashService = strHasher;
            emailSendService = emailSender;
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
		public JsonResult Register(RegisterModel account)
		{
			if (ModelState.IsValid)
			{
                try
                {
                    string salt = stringHashService.GetRandomSalt();
                    string hashedPassword = stringHashService.HashString(account.Password, salt);
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
                    emailSendService.SendVerificationLink(account.Username, account.Email);
                    return new JsonResult() { Data = "Signed-up" };
                }
                catch (InvalidOperationException)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
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
		public JsonResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = unitOfWork.UserRepository.Filter(model.Username);
				if(user != null)
				{
					if(stringHashService.ValidateHashedString(model.Password, user.Password, user.Salt))
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
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Entry");
		}
        [Route("Account/VerifyAccount/{username}/{salt}/{activationCode}")]
        public ActionResult VerifyAccount(string username, string salt, string activationCode)
        {
            User user = unitOfWork.UserRepository.Filter(username);
            if (user != null && stringHashService.ValidateHashedString(username, activationCode, salt))
            {
                user.IsVerified = true;
                unitOfWork.Commit();
                return View("VerifyAccount", (object)username);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View("Error");
            }
        }
	}
}