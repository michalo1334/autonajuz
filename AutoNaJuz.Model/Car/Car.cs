using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoNaJuz.Model.Car
{
    public class Car
    {
        [JsonConstructor]
        public Car(
            int id,
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

        public Car(
            string title,
            TransmissionType transmission,
            DateTime productionYear,
            FuelType fuelType,
            int seatCount,
            int doorCount,
            CarBodyType bodyType)
            : this(default, title, transmission, productionYear, fuelType, seatCount, doorCount, bodyType) {}

        public static Car Create(
            int id,
            string title,
            TransmissionType transmission,
            DateTime productionYear,
            FuelType fuelType,
            int seatCount,
            int doorCount,
            CarBodyType bodyType)
        {
            return new Car(
                id,
                title,
                transmission,
                productionYear,
                fuelType,
                seatCount,
                doorCount,
                bodyType
            );
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
    }
}