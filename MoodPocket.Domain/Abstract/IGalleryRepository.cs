using MoodPocket.Domain.Entities;
using System.Linq;

namespace MoodPocket.Domain.Abstract
{
	public interface IGalleryRepository
	{
		IQueryable<UserGallery> Galleries { get; }

		UserGallery GetOrCreate(User user);

		IQueryable<UserPicture> getAllGalleryPictures(User user);
	}
}
