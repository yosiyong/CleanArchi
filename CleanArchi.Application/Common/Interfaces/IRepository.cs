using CleanArchi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchi.Application.Common.Interfaces
{
	public interface IRepository<T> where T : class
	{

		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
		T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null);
		void Add(T entity);
		bool Any(Expression<Func<T, bool>> filter);
		void Remove(T entity);

		//void Update(T entity);
		//void Save();

		//Task<IList<T>> GetAllAsync();
		//Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
		//Task<T> GetAsync(int id);
		//Task<T> GetAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null);
		//Task<int> AddAsync(T entity);
		//Task<int> UpdateAsync(T entity);
		//Task<int> RemoveAsync(int id);

	}
}
