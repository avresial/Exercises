using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class BookingHelperTests
    {
        Mock<IBookingRepository> BookingRepository;

        public BookingHelperTests()
        {
            BookingRepository = new Mock<IBookingRepository>();
            Booking booking = new Booking();
            booking.ArrivalDate = new DateTime(2000, 1, 10);
            booking.DepartureDate = new DateTime(2000, 1, 15);
            booking.Reference = "DataBaseBooking";

            BookingRepository.Setup(x => x.GetActiveBookings(It.IsAny<int>())).Returns(new List<Booking>() { booking }.AsQueryable());
        }

        [Theory]
        [InlineData(8, 9)]
        [InlineData(16, 17)]
        public void OverlappingBookingsExist_BookingsDoNotOverlap_ReturnsEmptyString(int arrivalDay, int departureDay)
        {
            Booking booking = new Booking();
            booking.ArrivalDate = new DateTime(2000, 1, arrivalDay);
            booking.DepartureDate = new DateTime(2000, 1, departureDay);
            booking.Reference = "1";

            var result = BookingHelper.OverlappingBookingsExist(booking, BookingRepository.Object);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData(8, 11)]
        [InlineData(11, 12)]
        [InlineData(10, 15)]
        [InlineData(14, 17)]
        public void OverlappingBookingsExist_BookingsDoOverlap_ReturnsReferenceString(int arrivalDay, int departureDay)
        {
            Booking booking = new Booking();
            booking.ArrivalDate = new DateTime(2000, 1, arrivalDay);
            booking.DepartureDate = new DateTime(2000, 1, departureDay);
            booking.Reference = "1";

            var result = BookingHelper.OverlappingBookingsExist(booking, BookingRepository.Object);

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void OverlappingBookingsExist_BookingIsCanceled_ReturnsEmptyString()
        {
            Booking booking = new Booking();
            booking.ArrivalDate = new DateTime(2000, 1, 1);
            booking.DepartureDate = new DateTime(2000, 1, 31);
            booking.Reference = "1";
            booking.Status = "Cancelled";

            var result = BookingHelper.OverlappingBookingsExist(booking, BookingRepository.Object);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void OverlappingBookingsExist_AllBookingsInDatabaseAreCanceled_ReturnsReferenceString()
        {
            BookingRepository = new Mock<IBookingRepository>();
            Booking databaseBooking = new Booking();
            databaseBooking.ArrivalDate = new DateTime(2000, 1, 10);
            databaseBooking.DepartureDate = new DateTime(2000, 1, 15);
            databaseBooking.Reference = "DataBaseBooking";
            databaseBooking.Status = "Cancelled";

            BookingRepository.Setup(x => x.GetActiveBookings(It.IsAny<int>())).Returns(new List<Booking>() { databaseBooking }.AsQueryable());
            
            Booking booking = new Booking();
            booking.ArrivalDate = new DateTime(2000, 1, 1);
            booking.DepartureDate = new DateTime(2000, 1, 31);
            booking.Reference = "1";

            var result = BookingHelper.OverlappingBookingsExist(booking, BookingRepository.Object);

            Assert.Equal(string.Empty, result);
        }
    }
}
