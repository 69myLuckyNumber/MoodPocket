using MoodPocket.Domain.Abstract;
using System.Linq;
using MoodPocket.Domain.Entities;
using MoodPocket.Domain.Context;
using System;
using System.Collections.Generic;

namespace MoodPocket.Domain.Concrete
{
	public class GalleryRepository : IGalleryRepository
	{
		private DatabaseContext _context;

		public GalleryRepository(DatabaseContext context)
		{
			_context = context;
		}

		public IQueryable<Gallery> Galleries
		{
			get { return _context.Galleries; }
		}

		public IQueryable<Picture> GetAllPictures(int userId)
		{
			return _context.GalleryPictures.Where(g => g.Gallery.Id == userId).Select(p => p.Picture) 
				as IQueryable<Picture>;
		}

		public void DeletePicture(string url)
		{
			GalleryPicture gp = _context.GalleryPictures.Where(p => p.Picture.Url == url).FirstOrDefault();
			if (gp == null)
			{
				throw new InvalidOperationException();
			}
			else
			{
				_context.GalleryPictures.Remove(gp);
			}
			
		}

		public Gallery GetOrCreate(User user)
		{
			if (user.Gallery == null)
			{
				Gallery gallery = new Gallery()
				{
					GalleryPictures = new List<GalleryPicture>(),
					User = user,
					Id = user.Id,
					Name = "My Gallery"
				};
				_context.Galleries.Add(gallery);
				_context.SaveChanges();
			}
			return user.Gallery;
		}
	}
}
