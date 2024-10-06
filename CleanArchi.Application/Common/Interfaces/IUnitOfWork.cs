﻿namespace CleanArchi.Application.Common.Interfaces
{
	public interface IUnitOfWork
	{
		IVillaRepository Villa { get; }
		IVillaNumberRepository VillaNumber { get; }
        IAmenityRepository Amenity { get; }
        IBookingRepository Booking { get; }
        void Save();
	}
}
