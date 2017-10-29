using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		
		IRepository<User> UserRepository { get; }
		IRepository<Meme> MemeRepository { get; }
		IRepository<GalleryMeme> GalleryMemesRepository { get; }
		IRepository<Gallery> GalleryRepository { get; }

	}
}
