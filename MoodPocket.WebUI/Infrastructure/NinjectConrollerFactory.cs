using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Concrete;

using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using MoodPocket.WebUI.Utilities.Abstract;
using MoodPocket.WebUI.Utilities.Concrete;

namespace MoodPocket.WebUI.Infrastructure
{
	public class NinjectConrollerFactory : DefaultControllerFactory
	{
		private IKernel ninjectKernel;

		public NinjectConrollerFactory()
		{
			ninjectKernel = new StandardKernel();
			AddBindings();
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return controllerType == null ? null : ninjectKernel.Get(controllerType) as IController;
		}

		private void AddBindings()
		{
			ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
			ninjectKernel.Bind<ICacheService>().To<CacheService>();
            ninjectKernel.Bind<IStringHasher>().To<StringHashService>();
            ninjectKernel.Bind<IEmailSender>().To<EmailService>();
		}
	}
}