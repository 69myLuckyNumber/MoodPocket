using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Concrete;
using MoodPocket.Domain.Context;
using System;

namespace MoodPocket.WebUI.Utilities.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private DatabaseContext _context = new DatabaseContext();

		public UnitOfWork()
		{
			GalleryMemesRepository =	new GalleryMemeRepository(_context);
			MemeRepository =			new MemeRepository(_context);
			GalleryRepository =			new GalleryRepository(_context);
			UserRepository =			new UserRepository(_context);
			CurrentUserGetter =			new UserRepository(_context);
		}

		public IGalleryMemesRepository	GalleryMemesRepository { get; private set; }
		public IMemeRepository			MemeRepository { get; private set; }
		public IGalleryRepository		GalleryRepository { get; private set; }
		public IUserRepository			UserRepository { get; private set; }
		public IGetsCurrentUser			CurrentUserGetter { get; private set; }
	
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
