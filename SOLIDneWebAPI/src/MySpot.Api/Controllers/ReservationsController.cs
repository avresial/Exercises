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
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(reservations);

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var result = reservations.SingleOrDefault(x => x.Id == id);

            if (result is null)
                return NotFound();

            return Ok(result);
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

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

            if (existingReservation is null)
                return NotFound();

            existingReservation.LicensePlate = reservation.LicensePlate;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Put(int id)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == id);
           
            if (existingReservation is null)
                return NotFound();

            reservations.Remove(existingReservation);

            return NoContent();
        }

    }
}
