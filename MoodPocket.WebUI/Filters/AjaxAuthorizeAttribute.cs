using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Filters
{
	public class AjaxAuthorizeAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
				{
					var urlHelper = new UrlHelper(filterContext.RequestContext);
					filterContext.HttpContext.Response.SuppressFormsAuthenticati‌​onRedirect = true;
					filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
					filterContext.Result = new JsonResult()
					{
						Data = new
						{
							Message = "Join us",
							Url = urlHelper.Action("Entry", "Account")
						},
						JsonRequestBehavior = JsonRequestBehavior.AllowGet
					};
				}
			}
			else
			{
				base.OnActionExecuting(filterContext);
			}
			
		}
	}
}