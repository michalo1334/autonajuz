namespace AutoNaJuz.Model.Car;

public class Car
{
    public Car(int id,
        string title,
        TransmissionType transmission,
        DateTime productionYear,
        FuelType fuelType,
        int seatCount,
        int doorCount,
        CarBodyType bodyType)
    {
        Id = id;
        Title = title;
        Transmission = transmission;
        ProductionYear = productionYear;
        FuelType = fuelType;
        SeatCount = seatCount;
        DoorCount = doorCount;
        BodyType = bodyType;
    }

    public static Car Create(
        string title,
        TransmissionType transmission,
        DateTime productionYear,
        FuelType fuelType,
        int seatCount,
        int doorCount,
        CarBodyType bodyType)
    {
        return new Car(
            default,
            title,
            transmission,
            productionYear,
            fuelType,
            seatCount,
            doorCount,
            bodyType);
    }

    public void Update(
        string title,
        TransmissionType transmission,
        DateTime productionYear,
        FuelType fuelType,
        int seatCount,
        int doorCount,
        CarBodyType bodyType)
    {
        Title = title;
        Transmission = transmission;
        ProductionYear = productionYear;
        FuelType = fuelType;
        SeatCount = seatCount;
        DoorCount = doorCount;
        BodyType = bodyType;
    }
    
    public void UpdateImages(IEnumerable<CarImage.CarImage> images)
    {
        Images = images.ToList();
    }

    public int Id { get; set; }

    public string Title { get; private set; }
    public TransmissionType Transmission { get; private set; }
    public DateTime ProductionYear { get; private set; }
    public FuelType FuelType { get; private set; }
    public int SeatCount { get; private set; }
    public int DoorCount { get; private set; }
    public CarBodyType BodyType { get; private set; }

    //Navigation properties
    public IList<CarFeature> Features { get; set; } = [];
    public IList<CarRental.CarRental> Rentals { get; set; } = [];
    public IList<CarImage.CarImage> Images { get; set; } = [];
}