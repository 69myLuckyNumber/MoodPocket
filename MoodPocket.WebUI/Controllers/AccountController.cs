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
using AutoMapper;

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
				var user = unitOfWork.UserRepository
						.Get(u => u.Username == account.Username && u.Email == account.Email);
				if(user == null)
				{
					string salt = stringHashService.GetRandomSalt();
					string hashedPassword = stringHashService.HashString(account.Password, salt);

					account.Salt = salt;
					account.Password = account.ConfirmPassword = hashedPassword;
					var newUser = Mapper.Map<User>(account);



					unitOfWork.UserRepository.Add(newUser);
					unitOfWork.Commit();

					emailSendService.SendVerificationLink(account.Username, account.Email);
					return new JsonResult() { Data = "Confirmation email has been sent" };

				}
				else
				{
					HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				}
			}
			return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DoesUserExists(string Username, string Email)
		{
			User user = unitOfWork.UserRepository.Get(u => u.Username == Username && u.Email == Email);
			return Json(user == null);
		}

		[HttpPost]
		[AjaxValidate]
		[OnlyAnonymous]
		[ValidateAntiForgeryToken]
		public JsonResult Login(LoginModel model)
		{
			User user = unitOfWork.UserRepository.Get(u => u.Username == model.Username);
            if (ModelState.IsValid)
			{		
				if(user != null)
				{
                    if (user.IsVerified)
                    {
                        if (stringHashService.ValidateHashedString(model.Password, user.Password, user.Salt))
                        {
                            FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                            return new JsonResult() { Data = "Redirecting..." };
                        }
                    }
                    else
                    {
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return new JsonResult() { Data = "Verify your account" };
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
			User user = unitOfWork.UserRepository.Get(u => u.Username == username);
			if (user != null && !user.IsVerified && stringHashService.ValidateHashedString(username, activationCode, salt))
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