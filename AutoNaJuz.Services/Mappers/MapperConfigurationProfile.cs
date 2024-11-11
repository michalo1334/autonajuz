using AutoMapper;
using AutoNaJuz.Model;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Model.CarRental;
using AutoNaJuz.ViewModels.Car;
using AutoNaJuz.ViewModels.CarRental;
using AutoNaJuz.ViewModels.RenterInfo;

namespace AutoNaJuz.Services.Mappers;

public class MapperConfigurationProfile : Profile
{
    public MapperConfigurationProfile()
    {
        CreateMap<CarRental, EditCarRentalVm>()
            .ReverseMap();
        CreateMap<CarRental, CreateCarRentalVm>()
            .ReverseMap()
            .ConstructUsing(e => CarRental.Create(e.CarId,
                e.RenterId,
                e.PerHourCost,
                e.PerDayCost,
                e.From,
                e.To,
                e.Notes));
        
        CreateMap<Car, EditCarVm>()
            .ReverseMap();
        CreateMap<Car, CreateCarVm>()
            .ReverseMap()
            .ConstructUsing(e => Car.Create(e.Title,
                e.Transmission,
                e.ProductionYear,
                e.FuelType,
                e.SeatCount,
                e.DoorCount,
                e.BodyType));

        CreateMap<RenterInfo, EditRenterInfoVm>()
            .ReverseMap();
        CreateMap<RenterInfo, CreateRenterInfoVm>()
            .ReverseMap()
            .ConstructUsing(e => RenterInfo.Create(e.DrivingLicenseIdent,
                e.Pesel,
                e.BirthDate,
                e.FirstName,
                e.LastName,
                e.Street,
                e.BuildingNumber,
                e.ApartmentNumber,
                e.City,
                e.PostalCode));
    }
}