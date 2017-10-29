using MoodPocket.Domain.Entities;

using System.Data.Entity;


namespace MoodPocket.Domain.Context
{
	public class DatabaseContext : DbContext
	{
		private static DatabaseContext context;
        private DatabaseContext() : base("DatabaseContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }
		public static DatabaseContext GetInstance()
		{
			if(context == null)
			{
				context = new DatabaseContext();
			}
			return context;
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Gallery> Galleries { get; set; }

		public DbSet<Meme> Memes { get; set; }

		public DbSet<GalleryMeme> GalleryMemes { get; set; }

	}
}
