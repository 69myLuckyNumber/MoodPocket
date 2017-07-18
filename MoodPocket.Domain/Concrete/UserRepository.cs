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

		public void Complete()
		{
			context.SaveChanges();
		}

		public void CreateUser(User user)
		{
			context.Users.Add(user);
		}

		public User Get(int id)
		{
			return context.Users.Find(id);
		}

		public User Filter(string name = null, string email = null)
		{
			if (string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name))			// by name
			{
				return context.Users.FirstOrDefault(u => u.Username == name);
			}
			else if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))	// by email
			{
				return context.Users.FirstOrDefault(u => u.Email == email);
			}
			else if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(name))
			{
				return null;														// null
			}
			else
			{
				return context.Users.
					FirstOrDefault(u => u.Username == name || u.Email == email);	// by name and email
			}
			
		}
	}
}
