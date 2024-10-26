using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoNaJuz.Model
{
    public class CarRentalPrice
    {
        public int Id { get; set; }

        public decimal PerHourCost { get; set; }
        public decimal PerDayCost { get; set; }
    }
}