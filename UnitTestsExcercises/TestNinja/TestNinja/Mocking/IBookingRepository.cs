﻿using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? bookingId = null);
    }
}