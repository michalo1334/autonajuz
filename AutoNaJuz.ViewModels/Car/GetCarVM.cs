using AutoNaJuz.Model.Car;

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
    List<int> ImageIds
);