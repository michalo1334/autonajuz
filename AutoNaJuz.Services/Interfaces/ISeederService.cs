using AutoNaJuz.Model.Car;

namespace AutoNaJuz.Services.Interfaces;

public interface ISeederService
{
    public Task SeedCars();
    public Task SeedCarFeatures();
    public Task SeedRenterInfos();
    public Task AssignFeaturesToCars();
}