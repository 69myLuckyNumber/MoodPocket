using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Abstract
{
	public interface IGalleryPictureRepository
	{
		IQueryable<GalleryPicture> GalleryPictures { get; }

		void Create(GalleryPicture galleryPicture);

	}
}
