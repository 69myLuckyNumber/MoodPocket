using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

using MoodPocket.Domain.Abstract;
using MoodPocket.WebUI.App_Start;
using MoodPocket.WebUI.Utilities;
using MoodPocket.WebUI.Models;
using MoodPocket.Domain.Entities;

using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net;
using MoodPocket.WebUI.Filters;


namespace MoodPocket.WebUI.Controllers
{
	[AllowAnonymous]
	public class MemeController : Controller
    {
		private GalleryEndpoint galleryEndpoint; // api
		private AlbumEndpoint albumEndpoint;     // api

		private IUnitOfWork unitOfWork;

		private ICacheService cacheService;

        public ActionResult Index()
        {
            return View();
        }

		#region ImgurEndpointRegion
		[HttpPost]
		[Route("Meme/ShowMemes")]
		public async Task<JsonResult> ShowMemes()
		{
			return Json(await cacheService.GetOrSet("Memes", async () => await GetMemesAsync()));

		}
		private async Task<List<IImage>> GetMemesAsync()
		{
			var memeGallery = (await GetMemesSubGalleryAsync())
				.OfType<IGalleryAlbum>().ToList();
			var images = memeGallery.SelectMany(g => g.Images).ToList();
			return images;
		}
		
		private async Task<IEnumerable<IImage>> GetAlbumImagesAsync(string id)
		{
			return await albumEndpoint.GetAlbumImagesAsync(id);   // api
		}

		private async Task<IEnumerable<IGalleryItem>> GetMemesSubGalleryAsync()
		{
			return await galleryEndpoint.GetMemesSubGalleryAsync();   // api
		}
		private async Task<IEnumerable<IGalleryItem>> GetGalleryAsync()
		{
			return await galleryEndpoint.GetRandomGalleryAsync();   // api
		}

		#endregion
		
		[HttpPost]
		[AjaxAuthorize]
		[Route("Meme/SaveMeme")]
		public ActionResult SaveMeme(PictureModel picture)
		{
			try
			{
				User currentUser = unitOfWork.CurrentUserGetter.GetCurrentUser(HttpContext.User.Identity.Name);
				Gallery galleryDb = unitOfWork.GalleryRepository.GetOrCreate(currentUser);
				Picture pictureDb = unitOfWork.PictureRepository.GetOrCreate(picture.Url);

				unitOfWork.GalleryPictureRepository.Create(galleryDb, pictureDb);

				unitOfWork.Commit();
			}
			catch (InvalidOperationException)
			{
				HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return new JsonResult{ Data = "Already saved" };
			}

			return new JsonResult() { Data = "Saved" };
		}



		public MemeController(IUnitOfWork uow, ICacheService cacheServ)
		{
			var client = new ImgurClient(		
				ImgurClientConfig.IMGUR_CLIENT_ID,
				ImgurClientConfig.IMGUR_CLIENT_SECRET,
				ImgurClientConfig.AccessToken);				// api

			galleryEndpoint = new GalleryEndpoint(client);  // api
			albumEndpoint = new AlbumEndpoint(client);		// api

			unitOfWork = uow;

			cacheService = cacheServ;
		}
	}
}