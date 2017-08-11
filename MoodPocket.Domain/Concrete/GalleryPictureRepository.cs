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

		public void	Create(GalleryPicture galleryPicture)
		{
			_context.GalleryPictures.Add(galleryPicture);
			_context.Galleries.FirstOrDefault(g => g.GalleryID == galleryPicture.GalleryID).GalleryPictures.Add(galleryPicture);
			_context.Pictures.FirstOrDefault(g => g.PictureID == galleryPicture.PictureID).GalleryPictures.Add(galleryPicture);
		}
	}
}
