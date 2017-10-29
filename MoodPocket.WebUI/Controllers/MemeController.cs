using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

using MoodPocket.Domain.Abstract;
using MoodPocket.WebUI.App_Start;
using MoodPocket.WebUI.Utilities;
using MoodPocket.WebUI.Models;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Filters;
using MoodPocket.WebUI.Utilities.Abstract;

using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net;


namespace MoodPocket.WebUI.Controllers
{
	[AllowAnonymous]
	public class MemeController : Controller
    {
		private GalleryEndpoint galleryEndpoint; // api
		private AlbumEndpoint albumEndpoint;     // api

		private IUnitOfWork unitOfWork;

		private ICacheService cacheService;

        [Route("memes")]
        public ActionResult Index()
        {
            return View();
        }

		#region ImgurEndpointRegion
		[HttpPost]
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
		public ActionResult SaveMeme(MemeModel meme)
		{

			var currentUser = unitOfWork.UserRepository.Get(u => u.Username == HttpContext.User.Identity.Name);
			var gallery = unitOfWork.GalleryRepository.FindById(currentUser.Id);
			if(gallery == null)
			{
				gallery = unitOfWork.GalleryRepository.Add(new Gallery()
				{
					Id = currentUser.Id,
					Name = "Default",
					User = currentUser,
					GalleryMemes = new List<GalleryMeme>()
				});
			}
			var mem = unitOfWork.MemeRepository.Get(m => m.Url == meme.Url);
			if(mem == null)
			{
				mem = unitOfWork.MemeRepository.Add(new Meme()
				{
					Url = meme.Url,
					GalleryMemes = new List<GalleryMeme>()
				});
			}

			var memGallery = unitOfWork.GalleryMemesRepository
				.Get(g => g.GalleryID == gallery.Id && g.MemeID == mem.Id);
			if (memGallery == null)
			{
				memGallery = unitOfWork.GalleryMemesRepository.Add(new GalleryMeme()
				{
					Gallery = gallery,
					Meme = mem
				});
			}
			else
			{
				HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return new JsonResult() { Data = "Already saved" };
			}
			unitOfWork.Commit();
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