using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Infrastructure.Data;

namespace CleanArchi.Infrastructure.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		public IVillaRepository Villa {  get; private set; }
		public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepository(_db);
		}

		public void Save()
		{
			throw new NotImplementedException();
		}
	}
}
