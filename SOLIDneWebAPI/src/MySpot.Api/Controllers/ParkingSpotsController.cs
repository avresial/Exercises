using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using MySpot.Core.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("parking-spots")]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly IClock clock;
        private readonly ICommandHandler<ChangeReservationLicensePlate> _changeReservationLicensePlateHandler;
        private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
        private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotForCleaningHandler;
        private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotForVehicleHandler;
        private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _getWeeklyParkingSpotsHandler;

        public ParkingSpotsController(IClock clock, ICommandHandler<ChangeReservationLicensePlate> changeReservationLicensePlateHandler,
            ICommandHandler<DeleteReservation> deleteReservationHandler, ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotForCleaningHandler,
            ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler, IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler)
        {
            this.clock = clock;
            _changeReservationLicensePlateHandler = changeReservationLicensePlateHandler;
            _deleteReservationHandler = deleteReservationHandler;
            _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
            _reserveParkingSpotForVehicleHandler = reserveParkingSpotForVehicleHandler;
            _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots getWeeklyParkingSpots) =>
            Ok(await _getWeeklyParkingSpotsHandler.HandleAsync(getWeeklyParkingSpots));


        [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
        public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
        {
            await _reserveParkingSpotForVehicleHandler.HandleAsync(command with
            {
                ReservationId = Guid.NewGuid(),
                ParkingSpotId = parkingSpotId,
            });

            return NoContent();
        }



        [HttpPost("reservations/cleaning")]
        public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _reserveParkingSpotForCleaningHandler.HandleAsync(command);
            return Ok();
        }


        [HttpPut("reservations/{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ChangeReservationLicensePlate commmand)
        {
            await _changeReservationLicensePlateHandler.HandleAsync(commmand);
            return NoContent();
        }

        [HttpDelete("reservations/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _deleteReservationHandler.HandleAsync(new DeleteReservation(id));
            return NoContent();
        }
    }
}