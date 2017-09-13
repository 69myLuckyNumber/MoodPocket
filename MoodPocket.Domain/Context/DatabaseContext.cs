using MoodPocket.Domain.Entities;

using System.Data.Entity;


namespace MoodPocket.Domain.Context
{
	public class DatabaseContext : DbContext
	{

		public DbSet<User> Users { get; set; }

		public DbSet<Gallery> Galleries { get; set; }

		public DbSet<Meme> Memes { get; set; }

		public DbSet<GalleryMeme> GalleryMemes { get; set; }

	}
}
