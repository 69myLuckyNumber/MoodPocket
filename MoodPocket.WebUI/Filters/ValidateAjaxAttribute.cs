using System.Web.Mvc;
using System.Linq;
using System.Net;

namespace MoodPocket.WebUI.Filters
{
	public class ValidateAjaxAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				var modelState = filterContext.Controller.ViewData.ModelState;

				if (!modelState.IsValid)
				{
					var errorModel = from field in modelState.Keys
									 where modelState[field].Errors.Count > 0
									 select new
									 {
										 key = field,
										 errors = modelState[field].Errors
											.Select(f => f.ErrorMessage)
											.ToArray()

									 };

					filterContext.Result = new JsonResult() { Data = errorModel };

					filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				}
			}
		}
	}
}