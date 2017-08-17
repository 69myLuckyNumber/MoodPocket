using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Abstract
{
	public interface IPictureRepository
	{
		IQueryable<Picture> Pictures { get; }

		Picture GetOrCreate(string url);
	}
}
