using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.WebUI.Utilities.Abstract
{
    public interface ICacheService
    {
        Task<List<T>> GetOrSet<T>(string chacheKey, Func<Task<List<T>>> getItemsCallback) where T : class;
    }
}
