using Microsoft.AspNetCore.Mvc;
using MySpot.Application;
using MySpot.Application.Commands;
using MySpot.Application.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IClock clock;

        private readonly IReservationsService service;

        public ReservationsController(IClock clock, IReservationsService service)
        {
            this.clock = clock;
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> Get() => Ok(await service.GetAllWeeklyAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReservationDto>> Get(Guid id)
        {
            var reservation = await service.GetAsync(id);

            if (reservation is null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateReservationParkingSpot command)
        {
            var id = await service.CreateAsync(command with { ReservationId = Guid.NewGuid() });

            if (id is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = command.ReservationId }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ChangeReservationLicensePlate commmand)
        {
            if (!await service.UpdateAsync(commmand with { ReservationId = id }))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!await service.DeleteAsync(new DeleteReservation(id)))
                return NotFound();

            return NoContent();
        }
    }
}