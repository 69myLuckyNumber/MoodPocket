using MoodPocket.Domain.Abstract;
using System.Linq;
using MoodPocket.Domain.Entities;
using MoodPocket.Domain.Context;

namespace MoodPocket.Domain.Concrete
{
	public class GalleryRepository : IGalleryRepository
	{
		private DatabaseContext context = new DatabaseContext();

		public IQueryable<UserGallery> Galleries
		{
			get { return context.Galleries; }
		}

	}
}
