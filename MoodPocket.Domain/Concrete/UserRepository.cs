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
	public class UserRepository : IUserRepository
	{
		private DatabaseContext context = new DatabaseContext();

		public IQueryable<User> Users
		{
			get
			{
				return context.Users;
			}
		}

		public void CreateUserAndSave(User user)
		{
			context.Users.Add(user);
			context.SaveChanges();
		}
	}
}
