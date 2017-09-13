using MoodPocket.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodPocket.Domain.Entities;
using MoodPocket.Domain.Context;

namespace MoodPocket.Domain.Concrete
{
	public class GalleryMemeRepository : IGalleryMemesRepository
	{
		private DatabaseContext _context;

		public GalleryMemeRepository(DatabaseContext context)
		{
			_context = context;
		}

		public IQueryable<GalleryMeme> GalleryMemes
		{
			get	{ return _context.GalleryMemes; }
		}

		public void	Create(Gallery gallery, Meme meme)
		{
			if(_context.GalleryMemes.FirstOrDefault(g=>g.GalleryID == gallery.Id && g.MemeID == meme.Id) == null)
			{
				_context.GalleryMemes.Add(new GalleryMeme() { Gallery = gallery, Meme = meme });
				return;
			}
			throw new InvalidOperationException();
		}
	}
}
