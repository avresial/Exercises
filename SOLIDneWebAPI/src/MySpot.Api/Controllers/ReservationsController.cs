using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {

        private readonly ReservationsService service = new();

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(service.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = service.Get(id);

            if (reservation is null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            var id = service.Create(reservation);

            if (id is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            reservation.Id = id;

            if (!service.Updtae(reservation))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (!service.Delete(id))
                return NotFound();

            return NoContent();
        }

    }
}
