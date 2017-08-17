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
	public class GalleryPictureRepository : IGalleryPictureRepository
	{
		private DatabaseContext _context;

		public GalleryPictureRepository(DatabaseContext context)
		{
			_context = context;
		}

		public IQueryable<GalleryPicture> GalleryPictures
		{
			get	{ return _context.GalleryPictures; }
		}

		public void	Create(Gallery gallery, Picture picture)
		{
			if(_context.GalleryPictures.FirstOrDefault(g=>g.GalleryID == gallery.Id && g.PictureID == picture.Id) == null)
			{
				_context.GalleryPictures.Add(new GalleryPicture() { Gallery = gallery, Picture = picture });
				return;
			}
			throw new InvalidOperationException();
		}
	}
}
