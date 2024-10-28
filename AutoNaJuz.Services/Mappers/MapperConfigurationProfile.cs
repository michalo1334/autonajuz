using AutoMapper;
using AutoNaJuz.ViewModels.CarRental;
using AutoNaJuz.ViewModels.Car;

namespace AutoNaJuz.Services.Mappers
{
    public class MapperConfigurationProfile : Profile
    {
        public MapperConfigurationProfile()
        {
            CreateMap<Model.CarRental.CarRental, CreateOrEditCarRental>()
                .ReverseMap();
            CreateMap<Model.Car.Car, CreateOrEditCarVM>()
                .ReverseMap();
        }
    }
}