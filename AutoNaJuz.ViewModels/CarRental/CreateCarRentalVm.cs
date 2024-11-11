namespace AutoNaJuz.ViewModels.CarRental;

public record CreateCarRentalVm(
    int CarId,
    int RenterId,
    decimal PerHourCost,
    decimal PerDayCost,
    DateTime From,
    DateTime To,
    string? Notes
);