using MoodPocket.Domain.Entities;
using System.Linq;

namespace MoodPocket.Domain.Abstract
{
	public interface IGalleryRepository
	{
		IQueryable<Gallery> Galleries { get; }

		IQueryable<Meme> GetAllMemes(int userId);

		Gallery GetOrCreate(User user);

		void DeleteMeme(string url, string username);
	}
}
