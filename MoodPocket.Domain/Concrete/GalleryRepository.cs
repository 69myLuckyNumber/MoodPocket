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

		public IQueryable<UserGallery> Galleries
		{
			get { return _context.Galleries; }
		}

		public IQueryable<UserPicture> getAllPictures(int userId)
		{
			return _context.GalleryPictures.Where(g => g.Gallery.UserID == userId).Select(p => p.Picture) 
				as IQueryable<UserPicture>;
		}

		public void DeletePicture(string url)
		{
			GalleryPicture gp = _context.GalleryPictures.Where(p => p.Picture.Url == url).FirstOrDefault();
			if (gp == null) throw new InvalidOperationException();
			else
			{
				_context.GalleryPictures.Remove(gp);
			}
			
		}

		public UserGallery GetOrCreate(User user)
		{
			if (_context.Galleries.FirstOrDefault(g=>g.UserID == user.Id) == null)
			{
				UserGallery gallery = new UserGallery()
				{
					GalleryPictures = new List<GalleryPicture>(),
					User = user,
					UserID = user.Id,
					Name = "My Gallery"
				};
				_context.Galleries.Add(gallery);
				_context.Users.Find(user.Id).Galleries.Add(gallery);
				_context.SaveChanges();
			}
			return _context.Users.FirstOrDefault(u => u.Id == user.Id).Galleries.ElementAt(0);
		}
	}
}
