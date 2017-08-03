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
	public class GalleryRepository : IGalleryRepository
	{
		private DatabaseContext context = new DatabaseContext();

		public IQueryable<UserGallery> Galleries
		{
			get { return context.Galleries; }
		}

	}
}
