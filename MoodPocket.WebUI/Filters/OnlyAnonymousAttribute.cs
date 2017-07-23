using System.Web.Mvc;

namespace MoodPocket.WebUI.Filters
{
	public class OnlyAnonymousAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectResult("/");
			}
		}
	}
}