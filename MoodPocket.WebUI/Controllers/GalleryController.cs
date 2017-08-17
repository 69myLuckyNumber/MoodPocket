using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Controllers
{
    public class GalleryController : Controller
    {
		private IUnitOfWork unitOfWork;
		
		public GalleryController(IUnitOfWork uow)
		{
			unitOfWork = uow;
			
		}

		[HttpGet]
        public ActionResult Index()
        {
			User currentUser = currentUser = unitOfWork.CurrentUserGetter
				.GetCurrentUser(HttpContext.User.Identity.Name);
			var pictures = unitOfWork.GalleryRepository.GetAllPictures(currentUser.Id);

            return View(pictures);
        }

		[HttpPost]
		[AjaxAuthorize]
		public ActionResult DeleteMeme(string url)
		{
			try
			{
				unitOfWork.GalleryRepository.DeletePicture(url);
				unitOfWork.Commit();
			}
			catch (InvalidOperationException)
			{
				HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return new JsonResult { Data = "Already deleted" };
			}
			return new JsonResult { Data = "Deleted" };
		}
    }
}