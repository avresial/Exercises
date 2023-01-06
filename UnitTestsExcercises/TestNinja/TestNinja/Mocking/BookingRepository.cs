using System.Linq;

namespace TestNinja.Mocking
{
    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? bookingId = null)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            IQueryable<Booking> bookings = unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

            if (bookingId.HasValue)  return bookings.Where(y => y.Id != bookingId.Value);
            
            return bookings;
        }
    }
}
