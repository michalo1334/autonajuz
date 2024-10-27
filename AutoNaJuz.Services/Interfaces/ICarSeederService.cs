using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoNaJuz.Model.Car;

namespace AutoNaJuz.Services.Interfaces
{
    public interface ICarSeederService
    {
        public IEnumerable<Car> SeedRandom(int count);
    }
}