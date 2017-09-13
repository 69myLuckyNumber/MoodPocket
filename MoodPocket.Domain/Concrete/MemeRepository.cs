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
	public class MemeRepository : IMemeRepository
	{
		private DatabaseContext _context;

		public MemeRepository(DatabaseContext context)
		{
			_context = context;
		}

		public IQueryable<Meme> Memes
		{
			get { return _context.Memes; }
		}

		public Meme GetOrCreate(string url)
		{
			if(_context.Memes.FirstOrDefault(p=>p.Url == url) == null)
			{
				_context.Memes.Add(new Meme() { Url = url, GalleryMemes = new List<GalleryMeme>() });
				_context.SaveChanges();
			}
			return _context.Memes.FirstOrDefault(p => p.Url == url);
		}
	}
}
