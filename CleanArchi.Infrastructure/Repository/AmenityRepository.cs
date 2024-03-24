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
	public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
		private readonly ApplicationDbContext _db;
		public AmenityRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public void Update(Amenity entity)
        {
            _db.Amenities.Update(entity);
        }

        //public void Save()
        //{
        //	_db.SaveChanges();
        //}
    }
}
