using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
	public record ParkingSpotId
	{
		public Guid Value { get; }
		public ParkingSpotId(Guid value)
		{
			if (value == Guid.Empty)
				throw new InvalidEntityIdException(value);

			Value = value;
		}

		public static implicit operator Guid(ParkingSpotId licensePlate) => licensePlate.Value;
		public static implicit operator ParkingSpotId(Guid licensePlate) => new(licensePlate);
	}
}
