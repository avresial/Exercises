﻿using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions
{
    public class InvalidReservationDateException : CustomException
    {
        public DateTime Date { get; }
        public InvalidReservationDateException(DateTime date) : base($"Reservation date: {date} is invalid.")
        {
            Date = date;
        }
    }
}
