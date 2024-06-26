﻿using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
	public record ReservationId
	{
		public Guid Value { get; }
		public ReservationId(Guid value)
		{
			if (value == Guid.Empty)
				throw new InvalidEntityIdException(value);

			Value = value;
		}

		public static ReservationId Create() => new(Guid.NewGuid());

		public static implicit operator Guid(ReservationId id) => id.Value;
		public static implicit operator ReservationId(Guid id) => new(id);
	}
}
