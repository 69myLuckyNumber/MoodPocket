using System;
using System.Collections.Generic;

using MoodPocket.WebUI.Extensions;
using System.Threading.Tasks;
using System.Web;
using MoodPocket.WebUI.Utilities.Abstract;

namespace MoodPocket.WebUI.Utilities.Concrete
{
	public class CacheService : ICacheService
	{
		public async Task<List<T>> GetOrSet<T>(string cacheKey, Func<Task<List<T>>> getItemsCallback) where T : class
		{
			List<T> itemsList = HttpContext.Current.Session[cacheKey] as List<T>;

			if(itemsList.IsNullOrEmpty())
			{
				itemsList = await getItemsCallback();
				HttpContext.Current.Session.Add(cacheKey, itemsList);
				
			}
			return itemsList.TakeAndRemove(25);
		}	
	}
}