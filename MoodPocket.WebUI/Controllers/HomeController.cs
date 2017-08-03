using System.Web.Mvc;

namespace MoodPocket.WebUI.Controllers
{
	public class HomeController : Controller
	{
		
		public ActionResult Index()
		{
			return View();
		}
	}
}