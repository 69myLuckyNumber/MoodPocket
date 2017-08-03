using MoodPocket.Domain.Entities;
using System.Linq;

namespace MoodPocket.Domain.Abstract
{
	public interface IGalleryRepository
	{
		IQueryable<UserGallery> Galleries { get; }
	}
}
