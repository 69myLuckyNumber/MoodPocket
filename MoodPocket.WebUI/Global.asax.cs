using MoodPocket.Domain.Context;
using MoodPocket.WebUI.App_Start;
using MoodPocket.WebUI.Infrastructure;
using MoodPocket.WebUI.Infrastructure.Ninject;
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
			DependencyResolver.SetResolver(new NinjectDependencyResolver());
			MapperConfig.RegisterMappings();
			
			ImgurClientConfig.Init();
		}
	}
}
