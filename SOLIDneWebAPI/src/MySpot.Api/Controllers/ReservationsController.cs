using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers
{
	[ApiController]
	[Route("reservations")]
	public class ReservationsController : ControllerBase
	{
		private static readonly List<Reservation> reservations = new();
		private readonly List<string> parkingSpotNames = new()
		{
			"P1", "P2", "P3", "P4", "P5"
		};

		private static int id = 1;

		[HttpGet]
		public IEnumerable<Reservation> Get() => reservations;

		[HttpGet("{id:int}")]
		public IEnumerable<Reservation> Get(int id)
		{
			var result = reservations.Where(x => x.Id == id);

			if (result is null)
			{
				HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
				return default;
			}

			return result;
		}

		[HttpPost]
		public ActionResult Post(Reservation reservation)
		{
			if (parkingSpotNames.All(x => x != reservation.ParkingSpotName))
				return BadRequest();

			reservation.Date = DateTime.UtcNow.AddDays(1).Date;

			if (reservations.Any(x => x.ParkingSpotName == reservation.ParkingSpotName && x.Date == reservation.Date))
				return BadRequest();

			reservation.Id = id++;
			reservations.Add(reservation);

			return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
		}

	}
}
