using MoodPocket.Domain.Entities;
using System.Linq;

namespace MoodPocket.Domain.Abstract
{
	public interface IGalleryRepository
	{
		IQueryable<Gallery> Galleries { get; }

		IQueryable<Picture> GetAllPictures(int userId);

		Gallery GetOrCreate(User user);

		void DeletePicture(string url, string username);
	}
}
