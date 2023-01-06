using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class BookingHelperTests
    {
        Mock<IBookingRepository> BookingRepository;
        Booking databaseBooking;

        public BookingHelperTests()
        {
            BookingRepository = new Mock<IBookingRepository>();

            databaseBooking = new Booking();
            databaseBooking.ArrivalDate = new DateTime(2000, 1, 10);
            databaseBooking.DepartureDate = new DateTime(2000, 1, 15);
            databaseBooking.Reference = "DataBaseBooking";

            BookingRepository.Setup(x => x.GetActiveBookings(It.IsAny<int>())).Returns(new List<Booking>() { databaseBooking }.AsQueryable());
        }

        [Theory]
        [InlineData(8, 9)]
        [InlineData(16, 17)]
        public void OverlappingBookingsExist_BookingsDoNotOverlap_ReturnsEmptyString(int arrivalDay, int departureDay)
        {
            

            var result = BookingHelper.OverlappingBookingsExist(CreateBooking(arrivalDay, departureDay), BookingRepository.Object);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData(8, 11)]
        [InlineData(11, 12)]
        [InlineData(10, 15)]
        [InlineData(14, 17)]
        [InlineData(9, 17)]
        public void OverlappingBookingsExist_BookingsDoOverlap_ReturnsReferenceString(int arrivalDay, int departureDay)
        {
            var result = BookingHelper.OverlappingBookingsExist(CreateBooking(arrivalDay, departureDay), BookingRepository.Object);

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void OverlappingBookingsExist_BookingIsCanceled_ReturnsEmptyString()
        {
            Booking booking = CreateBooking(1, 31);
            booking.Status = "Cancelled";

            var result = BookingHelper.OverlappingBookingsExist(booking, BookingRepository.Object);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void OverlappingBookingsExist_AllBookingsInDatabaseAreCanceled_ReturnsReferenceString()
        {

            BookingRepository.Setup(x => x.GetActiveBookings(It.IsAny<int>())).Returns(new List<Booking>() {  }.AsQueryable());

            Booking booking = CreateBooking(10, 15);

            var result = BookingHelper.OverlappingBookingsExist(booking, BookingRepository.Object);

            Assert.Equal(string.Empty, result);
        }

        private Booking CreateBooking(int arrivalDay, int departureDay)
        {
            Booking booking = new Booking();
            booking.ArrivalDate = new DateTime(2000, 1, arrivalDay);
            booking.DepartureDate = new DateTime(2000, 1, departureDay);
            booking.Reference = "1";

            return booking;
        }

    }
}
