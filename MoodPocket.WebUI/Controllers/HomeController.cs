using MoodPocket.Domain.Abstract;
using MoodPocket.WebUI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Controllers
{
	public class HomeController : Controller
	{
        private IUnitOfWork unitOfWork;
        
        public HomeController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

		public ActionResult Index()
		{

            List<UserCard> userCard = new List<UserCard>();
            var users = unitOfWork.UserRepository.GetAllUsersWithMemes().ToList();
            foreach(var user in users)
            {
                userCard.Add(new UserCard()
                {
                    User = user,
                    Gallery = user.Gallery,
                    SavedMemesCount = user.Gallery.GalleryPictures.Count,
                    BackgroundPictureUrl = user.Gallery.GalleryPictures.First().Picture.Url
                });
            }
            HomeViewModel model = new HomeViewModel()
            {
                UserCards = userCard
            };

            return View(model);
		}
	}
}