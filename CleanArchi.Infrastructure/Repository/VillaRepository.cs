using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchi.Infrastructure.Repository
{
	public class VillaRepository : IVillaRepository
	{
		private readonly ApplicationDbContext _db;

		public VillaRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Add(Villa entity)
		{
			_db.Add(entity);
		}

		public Villa Get(Expression<Func<Villa, bool>>? filter, string? includeProperties = null)
		{
			IQueryable<Villa> query = _db.Set<Villa>();
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

		public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<Villa> query = _db.Set<Villa>();
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

		public void Remove(Villa entity)
		{
			_db.Remove(entity);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(Villa entity)
		{
			_db.Villas.Update(entity);
		}



		//public async Task<int> AddAsync(Villa entity)
		//{
		//	await _db.Villas.AddAsync(entity);
		//	await _db.SaveChangesAsync();
		//	return entity.Id;
		//}

		//public async Task<int> RemoveAsync(int id)
		//{
		//	var existingState = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
		//	if (existingState is null) throw new Exception("処理対象のデータがありません。");

		//	await Task.Run(() => { _db.Villas.Remove(existingState); });
		//	await _db.SaveChangesAsync();
		//	return id;
		//}

		//public void Save()
		//      {
		//          _db.SaveChanges();
		//      }

		//      public void Update(Villa entity)
		//      {
		//          _db.Villas.Update(entity);
		//      }


		//public async Task<IList<Villa>> GetAllAsync()
		//{
		//	return await _db.Villas.AsNoTracking().ToListAsync();
		//}

		//public async Task<IList<Villa>> GetAllAsync(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
		//{
		//	IQueryable<Villa> query = _db.Set<Villa>();

		//	if (filter != null)
		//	{
		//		query = query.Where(filter);
		//	}

		//	if (!string.IsNullOrEmpty(includeProperties))
		//	{
		//		foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		//		{
		//			query = query.Include(includeProp);
		//		}
		//	}

		//	return await query.ToListAsync();
		//}

		//public async Task<Villa> GetAsync(int id)
		//{
		//	return await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		//}

		//public async Task<Villa> GetAsync(Expression<Func<Villa, bool>>? filter, string? includeProperties = null)
		//{
		//	IQueryable<Villa> query = _db.Set<Villa>();
		//	if (filter != null)
		//	{
		//		query = query.Where(filter);
		//	}

		//	if (!string.IsNullOrEmpty(includeProperties))
		//	{
		//		foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		//		{
		//			query = query.Include(includeProp);
		//		}
		//	}

		//	return await query.FirstOrDefaultAsync();
		//}

		//public async Task<int> UpdateAsync(Villa villa)
		//{
		//	await Task.Run(() => { _db.Villas.Update(villa); });
		//	await _db.SaveChangesAsync();
		//	return villa.Id;
		//}
	}
}
