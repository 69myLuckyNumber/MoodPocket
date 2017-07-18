using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Abstract
{
	public interface IUserRepository
	{
		IQueryable<User> Users { get; }

		User Get(int id);

		User Filter(string name, string email);

		void CreateUser(User user);

		void Complete();

	}
}
