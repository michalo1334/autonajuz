using AutoNaJuz.Model.Car;

namespace AutoNaJuz.ViewModels.Car;

public record EditCarVm(
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