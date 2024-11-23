using System.Text.Json.Serialization;

namespace AutoNaJuz.Model.CarRental;

public class CarRental
{
    [JsonConstructor]
    public CarRental(
        int id,
        int carId,
        int renterId,
        decimal? perHourCost,
        decimal? perDayCost,
        DateTime from,
        DateTime to,
        string? notes)
    {
        Id = id;
        CarId = carId;
        RenterId = renterId;
        PerHourCost = perHourCost;
        PerDayCost = perDayCost;
        From = from;
        To = to;
        Notes = notes;
    }

    public static CarRental Create(
        int carId,
        int renterId,
        decimal? perHourCost,
        decimal? perDayCost,
        DateTime from,
        DateTime to,
        string? notes)
    {
        return new CarRental(
            0,
            carId,
            renterId,
            perHourCost,
            perDayCost,
            from,
            to,
            notes
        );
    }

    public void Update(
        int carId,
        int renterId,
        decimal? perHourCost,
        decimal? perDayCost,
        DateTime from,
        DateTime to,
        string? notes)
    {
        CarId = carId;
        RenterId = renterId;
        PerHourCost = perHourCost;
        PerDayCost = perDayCost;
        From = from;
        To = to;
        Notes = notes;
    }

    public int Id { get; set; }

    public int CarId { get; set; }
    public int RenterId { get; set; }
    public decimal? PerHourCost { get; private set; }
    public decimal? PerDayCost { get; private set; }
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }
    public string? Notes { get; private set; }

    //Navigation properties
    public Car.Car Car { get; set; } = default!;
    public RenterInfo Renter { get; set; } = default!;
}