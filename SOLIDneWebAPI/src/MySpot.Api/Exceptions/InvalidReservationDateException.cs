namespace MySpot.Api.Exceptions
{
    public class InvalidReservationDateException : CustomException
    {
        public DateTime Date { get; }
        public InvalidReservationDateException(DateTime date) : base($"Reservation date: {date.Date} is invalid.")
        {
            Date = date;
        }
    }

    public class ParkingSpotAlreadyReservedException : CustomException
    {
        public string Name { get; }
        public DateTime Date { get; }

        public ParkingSpotAlreadyReservedException(string name, DateTime date) : base($"Parking spot {name} is already reserved at {date.Date}.")
        {
            Name = name;
            Date = date;
        }
    }
}
