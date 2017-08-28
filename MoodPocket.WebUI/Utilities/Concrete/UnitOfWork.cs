﻿using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Context;
using System;

namespace MoodPocket.Domain.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private DatabaseContext _context = new DatabaseContext();

		public UnitOfWork()
		{
			GalleryPictureRepository =	new GalleryPictureRepository(_context);
			PictureRepository =			new PictureRepository(_context);
			GalleryRepository =			new GalleryRepository(_context);
			UserRepository =			new UserRepository(_context);
			CurrentUserGetter =			new UserRepository(_context);
		}

		public IGalleryPictureRepository	GalleryPictureRepository { get; private set; }
		public IPictureRepository			PictureRepository { get; private set; }
		public IGalleryRepository			GalleryRepository { get; private set; }
		public IUserRepository				UserRepository { get; private set; }
		public IGetsCurrentUser				CurrentUserGetter { get; private set; }
	
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