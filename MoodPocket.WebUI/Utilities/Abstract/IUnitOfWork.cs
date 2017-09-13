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
		IGalleryMemesRepository GalleryMemesRepository { get; }
		IMemeRepository MemeRepository { get; }
		IUserRepository UserRepository { get; }
		IGalleryRepository GalleryRepository { get; }
		IGetsCurrentUser CurrentUserGetter { get; }
	}
}
