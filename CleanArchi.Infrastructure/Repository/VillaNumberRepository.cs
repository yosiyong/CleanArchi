using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchi.Infrastructure.Repository
{
	public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
	{
		private readonly ApplicationDbContext _db;
		public VillaNumberRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


		public void Update(VillaNumber entity)
		{
			_db.VillaNumbers.Update(entity);
		}

		//public void Save()
		//{
		//	_db.SaveChanges();
		//}
	}
}
