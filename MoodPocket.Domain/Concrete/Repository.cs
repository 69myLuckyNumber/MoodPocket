using MoodPocket.Domain.Abstract;
using MoodPocket.Domain.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Concrete
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private DatabaseContext _context;
		private DbSet<TEntity> _entities;
		public Repository(DatabaseContext context)
		{
			_context = context;
			_entities = context.Set<TEntity>();
		}

		public IQueryable<TEntity> Entities
		{
			get { return _entities; }
		}

		public TEntity Add(TEntity entity)
		{ 
			return _entities.Add(entity);
		}
		public void Delete(params int[] id)
		{
			TEntity entity = FindById(id);
			Delete(entity);
		}
		public void Delete(TEntity entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_entities.Attach(entity);
			}
			_entities.Remove(entity);
		}

		public TEntity FindById(params object[] id)
		{
			return _entities.Find(id);
		}

		public TEntity Get(Expression<Func<TEntity, bool>> predicate = null)
		{
			if (predicate == null)
				return null;

			return _entities.FirstOrDefault(predicate);
		}

		public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
		{
			IQueryable<TEntity> query = _entities;

			if(predicate != null)
			{
				query = query.Where(predicate);
			}
			return query;
		}

		public void Update(TEntity entity)
		{
			_entities.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}
