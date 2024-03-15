using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchi.Infrastructure.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
			_db = db;
			dbSet = _db.Set<T>();
        }

		public void Add(T entity)
		{
			_db.Add(entity);
		}

		public T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		//public void Update(T entity)
		//{
		//	dbSet.Update(entity);
		//}


		//public async Task<int> AddAsync(T entity)
		//{
		//	await dbSet.AddAsync(entity);
		//	await _db.SaveChangesAsync();

		//	var idProperty = typeof(T).GetProperty("Id");
		//	if (idProperty != null)
		//	{
		//		return (int)idProperty.GetValue(entity);
		//	}
		//	else
		//	{
		//		throw new InvalidOperationException("Entity does not have an 'Id' property.");
		//	}
		//}

		//public Task<IList<T>> GetAllAsync()
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<T> GetAsync(int id)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<T> GetAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<int> RemoveAsync(int id)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<int> UpdateAsync(T entity)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
