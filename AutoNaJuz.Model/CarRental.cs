using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoNaJuz.Model
{
    public class CarRental
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public int UserId { get; set; }
        public int CarRentalPriceId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Notes { get; set; }

        //Navigation properties
        public Car Car { get; set; }
        public /*TODO*/ string User { get; set; }
        public CarRentalPrice RentalPrice { get; set;}
    }
}