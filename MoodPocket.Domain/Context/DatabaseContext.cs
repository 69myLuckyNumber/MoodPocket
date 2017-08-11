using MoodPocket.Domain.Entities;

using System.Data.Entity;


namespace MoodPocket.Domain.Context
{
	public class DatabaseContext : DbContext
	{

		public DbSet<User> Users { get; set; }

		public DbSet<UserGallery> Galleries { get; set; }

		public DbSet<UserPicture> Pictures { get; set; }

		public DbSet<GalleryPicture> GalleryPictures { get; set; }

	}
}
