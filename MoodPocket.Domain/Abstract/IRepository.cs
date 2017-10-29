using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Abstract
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> Entities { get; }
		IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
		TEntity Get(Expression<Func<TEntity, bool>> predicate);
		TEntity FindById(params object[] id);
		TEntity Add(TEntity entity);
		void Delete(TEntity entity);
		void Delete(params int[] id);
		void Update(TEntity entity);
	}
}
