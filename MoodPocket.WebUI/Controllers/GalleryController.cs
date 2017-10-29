using AutoMapper;
using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Filters;
using MoodPocket.WebUI.Models;
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
		[Route("gallery/{username}")]
        public ActionResult Details(string username)
        {
			User user = unitOfWork.UserRepository.Get(u => u.Username == username);

            if (user == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View("Error");
            }

			IQueryable<Meme> userMemes = unitOfWork.GalleryMemesRepository
				.Query(g => g.Gallery.Id == user.Id)
				.Select(g => g.Meme);

            GalleryViewModel model = new GalleryViewModel()
            {
                Memes = Mapper.Map<IEnumerable<MemeModel>>(userMemes),
                HostedBy = username
            }; 
            return View(model);
        }

		[HttpPost]
		[AjaxAuthorize]
        [Route("Gallery/DeleteMeme")]
		public ActionResult DeleteMeme(MemeModel picture)
		{
			var meme = unitOfWork.GalleryMemesRepository
				.Get(p => p.Meme.Url == picture.Url && p.Gallery.User.Username == HttpContext.User.Identity.Name);
			if(meme != null)
			{
				unitOfWork.GalleryMemesRepository.Delete(meme);
			}
			else
			{
				HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return new JsonResult { Data = "Already deleted" };
			}
				
			unitOfWork.Commit();
			return new JsonResult { Data = "Deleted" };
		}
    }
}