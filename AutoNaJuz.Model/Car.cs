using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoNaJuz.Model
{
    public class Car
    {
        public int Id { get; set; }

        public string Title { get; set;} 
        public TransmissionType Transmission { get; set; }
        public DateTime ProductionYear { get; set; }
        public FuelType FuelType { get; set; }
        public int SeatCount { get; set; }
        public int DoorCount { get; set; }
        public CarBodyType BodyType {get; set;}

        //Navigation properties
        public IList<CarFeature> Features { get; set; } = [];
        public IList<CarRental> Rentals { get; set; } = [];
    }
}