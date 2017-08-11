﻿using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Concrete;
using MoodPocket.WebUI.Utilities;
using MoodPocket.Domain.Context;

using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;


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
		}
	}
}