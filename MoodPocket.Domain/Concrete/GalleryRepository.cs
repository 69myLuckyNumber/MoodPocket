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

		public IQueryable<Meme> GetAllMemes(int userId)
		{
			return _context.GalleryMemes.Where(g => g.Gallery.Id == userId).Select(p => p.Meme) 
				as IQueryable<Meme>;
		}

		public void DeleteMeme(string url, string username)
		{
			GalleryMeme gp = _context.GalleryMemes.Where(p => p.Meme.Url == url && p.Gallery.User.Username == username)
                                                        .FirstOrDefault();
			if (gp == null)
			{
				throw new InvalidOperationException();
			}
			else
			{
				_context.GalleryMemes.Remove(gp);
			}
			
		}

		public Gallery GetOrCreate(User user)
		{
			if (user.Gallery == null)
			{
				Gallery gallery = new Gallery()
				{
					GalleryMemes = new List<GalleryMeme>(),
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
