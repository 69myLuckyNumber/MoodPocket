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
	public class UserRepository : IUserRepository, IGetsCurrentUser
	{
		private DatabaseContext _context;

		public UserRepository(DatabaseContext context)
		{
			_context = context;
		}

		public IQueryable<User> Users
		{
			get
			{
				return _context.Users;
			}
		}

		public void CreateUser(User user)
		{
			_context.Users.Add(user);
		}

		public User Get(int id)
		{
			return _context.Users.Find(id);
		}

		public User Filter(string name = null, string email = null)
		{
			if (string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name))			// by name
			{
				return _context.Users.FirstOrDefault(u => u.Username == name);
			}
			else if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))	// by email
			{
				return _context.Users.FirstOrDefault(u => u.Email == email);
			}
			else if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(name))
			{
				return null;														// null
			}
			else
			{
				return _context.Users.
					FirstOrDefault(u => u.Username == name || u.Email == email);	// by name and email
			}
			
		}

		public User GetCurrentUser(string name)
		{
			return Filter(name);
		}
	}
}
