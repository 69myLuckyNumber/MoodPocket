﻿using MoodPocket.Domain.Abstract;
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
			User user = unitOfWork.UserRepository.Filter(username);
			var pictures = unitOfWork.GalleryRepository.GetAllPictures(user.Id);

            return View(pictures);
        }

		[HttpPost]
		[AjaxAuthorize]
		[Route("Gallery/DeleteMeme")]
		public ActionResult DeleteMeme(PictureModel picture)
		{
			if(picture.HostedBy == HttpContext.User.Identity.Name)
			{
				try
				{
					unitOfWork.GalleryRepository.DeletePicture(picture.Url);
					unitOfWork.Commit();
				}
				catch (InvalidOperationException)
				{
					HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					return new JsonResult { Data = "Already deleted" };
				}
				return new JsonResult { Data = "Deleted" };
			}
			HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return new JsonResult { Data = "Wrong access" };
		}
    }
}