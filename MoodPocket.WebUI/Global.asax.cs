using MoodPocket.Domain.Context;
using MoodPocket.WebUI.App_Start;
using MoodPocket.WebUI.Infrastructure;


using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MoodPocket.WebUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            MapperConfig.RegisterMappings();
			ControllerBuilder.Current.SetControllerFactory(new NinjectConrollerFactory());
			ImgurClientConfig.Init();
		}
	}
}
