using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;

namespace CleanArchi.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Booking entity)
        {
            _db.Bookings.Update(entity);
        }
    }
}
