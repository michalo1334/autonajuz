using System.Globalization;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model;
using AutoNaJuz.Model.Car;
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
            RenterInfo.Create("EK855/09/7", "54050252214", DateTime.Parse("1950-12-15", CultureInfo.InvariantCulture), "Kajetan", "Satora", "Nowa", "15", "16", "Głogów", "46-557"),
            RenterInfo.Create("MW000/93/6", "18070168241", DateTime.Parse("1983-07-01", CultureInfo.InvariantCulture), "Borys", "Fraś", "Sybiraków", "37", null, "Ostrowiec Świętokrzyski", "67-467"),
            RenterInfo.Create(null, "65060880562", DateTime.Parse("1988-03-10", CultureInfo.InvariantCulture), "Sylwia", "Pacholczak", "Gołębia", "145", "5", "Jarosław", "14-610"),
            RenterInfo.Create("PP368/20/1", "53080964177", DateTime.Parse("1958-01-28", CultureInfo.InvariantCulture), "Tymon", "Wisz", "Starowiejska", "98", "40", "Tarnów", "52-161"),
            RenterInfo.Create("WE445/11/0", "20081668522", DateTime.Parse("1995-04-19", CultureInfo.InvariantCulture), "Łukasz", "Jaśkowiec", "Krasickiego", "155", null, "Kraśnik", "59-953"),
            RenterInfo.Create(null, "64081574627", DateTime.Parse("1956-09-26", CultureInfo.InvariantCulture), "Julita", "Haponiuk", "Wieniawskiego", "160", "14", "Oleśnica", "60-224"),
            RenterInfo.Create(null, "14041680549", DateTime.Parse("2001-12-01", CultureInfo.InvariantCulture), "Maciej", "Paluszak", "Złota", "114", "27", "Chrzanów", "37-647"),
            RenterInfo.Create(null, "89012413374", DateTime.Parse("1952-12-29", CultureInfo.InvariantCulture), "Sylwia", "Szymula", "Wspólna", "167", "28", "Będzin", "95-460"),
            RenterInfo.Create("HE191/87/6", "99091340838", DateTime.Parse("2002-06-07", CultureInfo.InvariantCulture), "Leonard", "Labudda", "Łanowa", "188", "2", "Pruszcz Gdański", "78-119"),
            RenterInfo.Create(null, "27050271985", DateTime.Parse("1982-06-06", CultureInfo.InvariantCulture), "Julianna", "Związek", "Młynarska", "126", null, "Ełk", "34-122"),
            RenterInfo.Create(null, "23120451435", DateTime.Parse("2004-09-15", CultureInfo.InvariantCulture), "Cyprian", "Wylegała", "Cegielniana", "188", null, "Wągrowiec", "63-340"),
            RenterInfo.Create(null, "13252648520", DateTime.Parse("1962-12-30", CultureInfo.InvariantCulture), "Aniela", "Dudko", "Jagiellońska", "156", "32", "Sochaczew", "45-166"),
            RenterInfo.Create("RW972/76/1", "19041441570", DateTime.Parse("1985-09-22", CultureInfo.InvariantCulture), "Kaja", "Pajek", "Starowiejska", "193", "40", "Wyszków", "70-291"),
            RenterInfo.Create("KJ622/98/3", "46032132670", DateTime.Parse("1969-12-19", CultureInfo.InvariantCulture), "Maciej", "Mak", "Skłodowskiej-Curie", "136", "44", "Polkowice", "29-800"),
            RenterInfo.Create("FV587/00/2", "51080463344", DateTime.Parse("1964-06-11", CultureInfo.InvariantCulture), "Krystyna", "Stawowczyk", "Okrzei", "200", "19", "Zakopane", "16-402"),
            RenterInfo.Create(null, "63050240730", DateTime.Parse("1997-11-26", CultureInfo.InvariantCulture), "Sylwia", "Fijak", "Szczęśliwa", "139", "39", "Skawina", "96-084"),
            RenterInfo.Create("EE420/62/2", "23241516330", DateTime.Parse("1965-11-10", CultureInfo.InvariantCulture), "Mieszko", "Karwan", "Rolna", "5", "41", "Wodzisław Śląski", "87-317"),
            RenterInfo.Create("AI331/07/0", "88013013156", DateTime.Parse("1949-05-05", CultureInfo.InvariantCulture), "Stanisław", "Wasil", "Zbożowa", "63", null, "Lubliniec", "92-476"),
            RenterInfo.Create(null, "03300585240", DateTime.Parse("1981-12-28", CultureInfo.InvariantCulture), "Borys", "Tarach", "Okrzei", "121", "33", "Lębork", "79-214"),
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