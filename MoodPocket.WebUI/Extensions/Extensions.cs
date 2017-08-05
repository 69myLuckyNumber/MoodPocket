using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodPocket.WebUI.Extensions
{
	public static class Extensions
	{
		#region Extension methods for List
		/// <summary>
		/// Takes first quantity of objects and then removes it.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="quantity"></param>
		/// <returns>Returns taken objects.</returns>
		public static List<T> TakeAndRemove<T>(this List<T> list, int quantity) where T : class
		{
			List<T> temp = new List<T>();
			if (list.Count > quantity)
			{
				temp.AddRange(list.GetRange(0, quantity));
				list.RemoveRange(0, quantity);
			}
			else
			{
				temp.AddRange(list.GetRange(0, list.Count - 1));
				list.RemoveRange(0, list.Count - 1);
			}
			return temp;
		}
		/// <summary>
		/// Checks whether list is null or empty.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lst"></param>
		/// <returns>Returns bool.</returns>
		public static bool IsNullOrEmpty<T>(this List<T> list)
		{
			if (list == null)
				return true;

			return !list.Any();
		}
		#endregion
	}
}