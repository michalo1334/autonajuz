using AutoNaJuz.Model.Car;

namespace AutoNaJuz.Services.Interfaces;

public interface ICarSeederService
{
    public IEnumerable<Car> SeedRandom(int count);
}