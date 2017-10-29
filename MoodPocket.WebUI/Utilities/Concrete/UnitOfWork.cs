using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Concrete;
using MoodPocket.Domain.Context;
using System;
using MoodPocket.Domain.Entities;
using Ninject;

namespace MoodPocket.WebUI.Utilities.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private DatabaseContext _context;

		public UnitOfWork(DatabaseContext context, IRepository<User> ur, IRepository<Meme> mr,
			IRepository<GalleryMeme> gmr, IRepository<Gallery> gr)
		{
            _context = context;
			UserRepository = ur;
			MemeRepository = mr;
			GalleryMemesRepository = gmr;
			GalleryRepository = gr;
		}
		public IRepository<User> UserRepository { get; private set; }
		public IRepository<Meme> MemeRepository { get; private set; }
		public IRepository<GalleryMeme> GalleryMemesRepository { get; private set; }
		public IRepository<Gallery> GalleryRepository { get; private set; }

		public void Commit()
		{
			_context.SaveChanges();
		}

		#region IDisposable Support
		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}
}
