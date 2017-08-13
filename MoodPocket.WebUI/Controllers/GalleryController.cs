using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
			var pictures = unitOfWork.GalleryRepository.getAllPictures(currentUser.Id);

            return View(pictures);
        }
    }
}