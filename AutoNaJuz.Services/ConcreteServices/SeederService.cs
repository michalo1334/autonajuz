using System.Globalization;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Model.RenterInfo;
using AutoNaJuz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services.ConcreteServices;

public class SeederService(
    AppDbContext context) : ISeederService
{
    public async Task SeedRenterInfos()
    {
        List<RenterInfo> records =
        [

        ];
        
        context.RenterInfos.AddRange(records);
        await context.SaveChangesAsync();
    }

    public async Task SeedCarFeatures()
    {
        List<CarFeature> records =
        [
            CarFeature.Create("Klimatyzacja"),
            CarFeature.Create("Podgrzewane fotele"),
            CarFeature.Create("Nawigacja"),
            CarFeature.Create("Asystent parkowania"),
            CarFeature.Create("Tempomat"),
            CarFeature.Create("Radio"),
            CarFeature.Create("Kamera cofania"),
            CarFeature.Create("Radio Maryja"),
            CarFeature.Create("ABS"),
            CarFeature.Create("Opony z odzyskanych butelek PET"),
            CarFeature.Create("Kierownica z drewna"),
            CarFeature.Create("Kierownica z piłką do koszykówki"),
            CarFeature.Create("Silnik na popiół"),
            CarFeature.Create("Drzwi wejściowo-wyjściowe"),
            CarFeature.Create("Kotwica"),
            CarFeature.Create("Drzwi z przodu"),
            CarFeature.Create("Drzwi z tyłu"),
            CarFeature.Create("Drzwi z boku"),
            CarFeature.Create("Drzwi z góry"),
            CarFeature.Create("Drzwi z dołu"),
            CarFeature.Create("Brak kierunkowskazów"),
            CarFeature.Create("Tajemnicza plama na siedzeniu"),
            CarFeature.Create("Toaleta w bagażniku"),
            CarFeature.Create("Policjant w bagażniku"),
        ];
        
        context.CarFeatures.AddRange(records);
        await context.SaveChangesAsync();
    }

    public async Task SeedCars()
    {
        List<Car> records =
        [
            Car.Create("Opel Astra", TransmissionType.Automatic, DateTime.Parse("2024-01-01"), FuelType.Diesel, 2, 4,
                CarBodyType.Combi, 160),
            Car.Create("Fiat 126p", TransmissionType.Manual, DateTime.Parse("1975-01-01"), FuelType.HybridDieselEv, 4,
                2, CarBodyType.Sedan, 50),
            Car.Create("Ford Mustang", TransmissionType.Manual, DateTime.Parse("2010-01-01"), FuelType.Gas, 2, 2,
                CarBodyType.Hatchback, 300),
            Car.Create("Toyota Corolla", TransmissionType.Automatic, DateTime.Parse("2015-01-01"), FuelType.Electric, 5,
                4, CarBodyType.Suv, 200),
            Car.Create("Volkswagen Golf", TransmissionType.Manual, DateTime.Parse("2018-01-01"), FuelType.Lpg, 5, 4,
                CarBodyType.Van, 180),
            Car.Create("Mercedes-Benz S-Class", TransmissionType.Automatic, DateTime.Parse("2020-01-01"), FuelType.Gas, 5,
                4, CarBodyType.Sedan, 400),
            Car.Create("Audi A4", TransmissionType.Manual, DateTime.Parse("2019-01-01"), FuelType.HybridGasEv, 5, 4,
                CarBodyType.Combi, 250),
            Car.Create("BMW 3 Series", TransmissionType.Automatic, DateTime.Parse("2017-01-01"), FuelType.HybridLpgEv, 5,
                4, CarBodyType.Suv, 220),
            Car.Create("Porsche 911", TransmissionType.Manual, DateTime.Parse("2016-01-01"), FuelType.Diesel, 2, 2,
                CarBodyType.Hatchback, 350),
            Car.Create("Chevrolet Camaro", TransmissionType.Automatic, DateTime.Parse("2014-01-01"), FuelType.Gas, 2, 2,
                CarBodyType.Sedan, 300),
            Car.Create("Dodge Challenger", TransmissionType.Manual, DateTime.Parse("2013-01-01"), FuelType.Electric, 2, 2,
                CarBodyType.Van, 280),
            Car.Create("Jeep Wrangler", TransmissionType.Automatic, DateTime.Parse("2012-01-01"), FuelType.Lpg, 5, 4,
                CarBodyType.Suv, 220),
        ];

        context.Cars.AddRange(records);
        await context.SaveChangesAsync();
    }

    public async Task AssignFeaturesToCars()
    {
        //0-4 features per car, chosen randomly
        var cars = await context.Cars.ToListAsync();
        var features = await context.CarFeatures.ToListAsync();
        var random = new Random();
        foreach (var car in cars)
        {
            var featuresCount = random.Next(5);
            var carFeatures = features.OrderBy(x => random.Next()).Take(featuresCount).ToList();
            car.UpdateFeatures(carFeatures);
        }
        
        await context.SaveChangesAsync();
    }
}