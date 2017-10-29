using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Concrete;
using MoodPocket.Domain.Context;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Utilities.Abstract;
using MoodPocket.WebUI.Utilities.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Infrastructure.Ninject
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver()
		{
			kernel = new StandardKernel();
			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
		private void AddBindings()
		{
			kernel.Bind<ICacheService>().To<CacheService>();
			kernel.Bind<IStringHasher>().To<StringHashService>();
			kernel.Bind<IEmailSender>().To<EmailService>();


			kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>))
				.WithConstructorArgument("context", DatabaseContext.GetInstance());
			kernel.Bind<IUnitOfWork>().To<UnitOfWork>()
				.InSingletonScope()
				.WithConstructorArgument("context", DatabaseContext.GetInstance())
				.WithConstructorArgument("ur",kernel.Get<IRepository<User>>())
				.WithConstructorArgument("mr", kernel.Get<IRepository<Meme>>())
				.WithConstructorArgument("gmr", kernel.Get<IRepository<GalleryMeme>>())
				.WithConstructorArgument("gr", kernel.Get<IRepository<Gallery>>());


        }
	}
}