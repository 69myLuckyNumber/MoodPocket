using System;
using System.Collections.Generic;

using MoodPocket.WebUI.Extensions;
using System.Threading.Tasks;
using System.Web;

namespace MoodPocket.WebUI.Utilities
{
	public class CacheService : ICacheService
	{
		public async Task<List<T>> GetOrSet<T>(string cacheKey, Func<Task<List<T>>> getItemsCallback) where T : class
		{
			List<T> itemsList = HttpContext.Current.Session[cacheKey] as List<T>;

			if(itemsList.IsNullOrEmpty())
			{
				itemsList = await getItemsCallback() as List<T>;
				HttpContext.Current.Session.Add(cacheKey, itemsList);
				
			}
			return itemsList.TakeAndRemove(25);
		}

		
	}
	public interface ICacheService
	{
		Task<List<T>> GetOrSet<T>(string chacheKey, Func<Task<List<T>>> getItemsCallback) where T : class;
	}
}