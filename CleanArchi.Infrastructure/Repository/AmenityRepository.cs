using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;

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
