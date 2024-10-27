using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoNaJuz.Model.CarRental
{
    public class CarRental
    {
        [JsonConstructor]
        public CarRental(
            int id,
            int carId,
            string userId,
            decimal? perHourCost,
            decimal? perDayCost,
            DateTime from,
            DateTime to,
            string? notes)
        {
            Id = id;
            CarId = carId;
            UserId = userId;
            PerHourCost = perHourCost;
            PerDayCost = perDayCost;
            From = from;
            To = to;
            Notes = notes;
        }

        public static CarRental Create(
            int id,
            int carId,
            string userId,
            decimal? perHourCost,
            decimal? perDayCost,
            DateTime from,
            DateTime to,
            string? notes)
        {
            return new CarRental(
                id,
                carId,
                userId,
                perHourCost,
                perDayCost,
                from,
                to,
                notes
            );
        }

        public void Update(
            int id,
            int carId,
            string userId,
            decimal? perHourCost,
            decimal? perDayCost,
            DateTime from,
            DateTime to,
            string? notes)
        {
            Id = id;
            CarId = carId;
            UserId = userId;
            PerHourCost = perHourCost;
            PerDayCost = perDayCost;
            From = from;
            To = to;
            Notes = notes;
        }

        public int Id { get; set; }

        public int CarId { get; set; }
        public string UserId { get; set; }
        public decimal? PerHourCost { get; private set; }
        public decimal? PerDayCost { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public string? Notes { get; private set; }

        //Navigation properties
        public Car.Car Car { get; set; }
        public CustomerUser User { get; set; }
    }
}