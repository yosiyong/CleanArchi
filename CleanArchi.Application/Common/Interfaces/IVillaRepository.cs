using CleanArchi.Domain.Entities;
using System.Linq.Expressions;

namespace CleanArchi.Application.Common.Interfaces
{
    public interface IVillaRepository: IRepository<Villa>
    {

		void Update(Villa entity);
		void Save();


		//IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null);
		//Villa Get(Expression<Func<Villa, bool>>? filter, string? includeProperties = null);
		//void Add(Villa entity);
		//void Update(Villa entity);
		//void Remove(Villa entity);
		//void Save();


		//Task<IList<Villa>> GetAllAsync();
		//Task<IList<Villa>> GetAllAsync(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null);
		//Task<Villa> GetAsync(int id);
		//Task<Villa> GetAsync(Expression<Func<Villa, bool>>? filter, string? includeProperties = null);
		//Task<int> AddAsync(Villa villa);
		//      Task<int> UpdateAsync(Villa villa);
		//      Task<int> RemoveAsync(int id);

		//IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null);
		//Villa Get(Expression<Func<Villa, bool>>? filter, string? includeProperties = null);
		//void Add(Villa entity);
		//void Update(Villa entity);
		//void Remove(Villa entity);
		//void Save();
	}
}
