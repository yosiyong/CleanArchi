using CleanArchi.Domain.Entities;

namespace CleanArchi.Application.Common.Interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
	{
		void Update(Amenity entity);
	}
}
