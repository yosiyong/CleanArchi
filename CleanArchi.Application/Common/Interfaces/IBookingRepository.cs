using CleanArchi.Domain.Entities;

namespace CleanArchi.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);
    }
}
