using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
	public record EmployeeName
	{
		public string Value { get; }
		public EmployeeName(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new InvalidEmployeeNameException();

			Value = value;
		}

		public static implicit operator string(EmployeeName licensePlate) => licensePlate?.Value;
		public static implicit operator EmployeeName(string licensePlate) => new(licensePlate);
	}
}
