using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Entities;
using MoodPocket.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MoodPocket.Domain.Concrete
{
	public class PictureRepository : IPictureRepository
	{
		private DatabaseContext _context;

		public PictureRepository(DatabaseContext context)
		{
			_context = context;
		}

		public IQueryable<UserPicture> Pictures
		{
			get { return _context.Pictures; }
		}

		public UserPicture GetOrCreate(UserPicture picture)
		{
			if(_context.Pictures.FirstOrDefault(p=>p.Url == picture.Url) == null)
			{
				_context.Pictures.Add(picture);
				_context.SaveChanges();
			}
			return _context.Pictures.FirstOrDefault(p => p.Url == picture.Url);
		}
	}
}
