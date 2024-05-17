using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
	public record ParkingSpotName
	{
		public string Value { get; }
		public ParkingSpotName(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new InvalidParkingSpotNameException();

			Value = value;
		}

		public static implicit operator string(ParkingSpotName licensePlate) => licensePlate?.Value;
		public static implicit operator ParkingSpotName(string licensePlate) => new(licensePlate);
	}
}
