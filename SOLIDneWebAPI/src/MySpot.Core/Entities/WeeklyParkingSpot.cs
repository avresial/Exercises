using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = new();
    public const int MaxCapacity = 2;

    public ParkingSpotId Id { get; private set; }
    public Week Week { get; private set; }
    public ParkingSpotName Name { get; private set; }
    public Capacity Capacity { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;
    private WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name, Capacity capacity)
    {
        Id = id;
        Week = week;
        Name = name;
        Capacity = capacity;
    }

    public static WeeklyParkingSpot Create(ParkingSpotId id, Week week, ParkingSpotName name) => new WeeklyParkingSpot(id, week, name, MaxCapacity);

    internal void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = (reservation.Date < Week.From || reservation.Date > Week.To || reservation.Date < now);

        if (isInvalidDate) throw new InvalidReservationDateException(reservation.Date.Value.Date);


        var dateCapacity = Reservations.Where(x => x.Date == reservation.Date).Sum(x => x.Capacity);
        if (dateCapacity + reservation.Capacity > Capacity)
            throw new ParkingSpotCapacityExceededException(Id);

        _reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        if (!_reservations.Contains(reservation))
            throw new Exception("Reservation does not exist.");

        _reservations.Remove(reservation);
    }

    public void RemoveReservation(ReservationId id)
    {
        _reservations.RemoveWhere(x => x.Id == id);
    }

    public void RemoveReservations(IEnumerable<Reservation> reservations)
    {
        _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
    }
}
