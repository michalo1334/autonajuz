using AutoMapper;
using AutoNaJuz.Model;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Model.CarRental;
using AutoNaJuz.Model.RenterInfo;
using AutoNaJuz.ViewModels.Car;
using AutoNaJuz.ViewModels.CarRental;
using AutoNaJuz.ViewModels.RenterInfo;

namespace AutoNaJuz.Services.Mappers;

public class MapperConfigurationProfile : Profile
{
    public MapperConfigurationProfile()
    {
        CreateMap<CarRental, CreateCarRentalVm>()
            .ReverseMap()
            .ConstructUsing(e => CarRental.Create(e.CarId,
                e.RenterId,
                e.From,
                e.To,
                e.Notes));

        CreateMap<Car, CreateCarVm>()
            .ReverseMap()
            .ConstructUsing(e => Car.Create(e.Title,
                e.Transmission,
                e.ProductionYear,
                e.FuelType,
                e.SeatCount,
                e.DoorCount,
                e.BodyType,
                e.RentCostPerDay));
        
        CreateMap<RenterInfo, CreateRenterInfoVm>()
            .ReverseMap()
            .ConstructUsing(e => RenterInfo.Create(e.Phone,
                e.Email,
                e.FullName));
    }
}