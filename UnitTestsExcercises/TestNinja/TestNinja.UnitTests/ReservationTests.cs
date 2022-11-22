using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class ReservationTests
    {
        [Fact]
        public void CanBeCancelledBy_AdminCanceling_ReturnsTrue()
        {
            // Arrange
            Reservation reservation = new Reservation();

            // Act
            bool result = reservation.CanBeCancelledBy(new User() { IsAdmin = true });

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeCancelledBy_SameUserCanceling_ReturnsTrue()
        {
            // Arrange
            Reservation reservation = new Reservation() { MadeBy = new User() };

            // Act
            bool result = reservation.CanBeCancelledBy(reservation.MadeBy);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeCancelledBy_NotTheSameUserCanceling_ReturnsFalse()
        {
            // Arrange
            Reservation reservation = new Reservation() { MadeBy = new User() };

            // Act
            bool result = reservation.CanBeCancelledBy(new User());

            // Assert
            Assert.False(result);
        }

    }
}
