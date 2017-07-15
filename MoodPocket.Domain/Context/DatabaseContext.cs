using MoodPocket.Domain.Entities;

using System.Data.Entity;


namespace MoodPocket.Domain.Context
{
	public class DatabaseContext : DbContext
	{

		public DbSet<User> Users { get; set; }

	}
}
