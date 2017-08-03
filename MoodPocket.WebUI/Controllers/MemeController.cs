using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using MoodPocket.Domain.Abstract;
using MoodPocket.WebUI.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Controllers
{
	public class MemeController : Controller
    {
		private GalleryEndpoint endpoint;				// api
		private IGalleryRepository galleryRepository;
		
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public async Task<ActionResult> ShowMemes()
		{
			IEnumerable<IGalleryItem> gallery = await GetGalleryAsync();

			List<IGalleryItem> images = gallery.Where(t => t.GetType().Name == "GalleryImage").ToList();

			return Json(images);

		}
		//public ActionResult SaveMeme()
		//{
		//	return
		//}

		private async Task<IEnumerable<IGalleryItem>> GetGalleryAsync() 
		{
			return await endpoint.GetRandomGalleryAsync();   // api
		}

		public MemeController(IGalleryRepository galleryRepo)
		{
			var client = new ImgurClient(		
				ImgurClientConfig.IMGUR_CLIENT_ID,
				ImgurClientConfig.IMGUR_CLIENT_SECRET,
				ImgurClientConfig.AccessToken);				// api

			endpoint = new GalleryEndpoint(client);         // api
			galleryRepository = galleryRepo;

		}
	}
}