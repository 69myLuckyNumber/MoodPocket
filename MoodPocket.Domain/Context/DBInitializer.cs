
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Context
{
	public class DBInitializer : DropCreateDatabaseAlways<DatabaseContext>
	{
		protected override void Seed(DatabaseContext context)
		{
			context.Users.Add(new Entities.User() { Id = 0, Email = "Hello@mail.ru", ConfirmPassword = "123456", Password = "123456", Username = "qwerty" });
			base.Seed(context);
		}
	}
}
