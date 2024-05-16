using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Dtos;
using MySpot.Api.Entities;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {

        private readonly ReservationsService service = new();

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(service.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<ReservationDto> Get(Guid id)
        {
            var reservation = service.Get(id);

            if (reservation is null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(CreateReservationParkingSpot command)
        {
            var id = service.Create(command with { ReservationId = Guid.NewGuid() });

            if (id is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = command.ReservationId }, null);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, ChangeReservationLicensePlate commmand)
        {


            if (!service.Updtae(commmand with { ReservationId = id }))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if (!service.Delete(new DeleteReservation(id)))
                return NotFound();

            return NoContent();
        }

    }
}
