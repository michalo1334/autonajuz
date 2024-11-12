using AutoNaJuz.Model.Car;
using AutoNaJuz.ViewModels.CarRental;

namespace AutoNaJuz.ViewModels.Car;

public sealed record GetCarVm(
    int Id,
    string Title,
    TransmissionType Transmission,
    DateTime ProductionYear,
    FuelType FuelType,
    int SeatCount,
    int DoorCount,
    CarBodyType BodyType,
    IEnumerable<GetCarFeatureVm> Features,
    IEnumerable<GetCarRentalVm> Rentals
);