namespace AutoNaJuz.ViewModels.CarRental;

public sealed record EditCarRentalVm(
    int Id,
    int CarId,
    int RenterId,
    DateTime From,
    DateTime To,
    string? Notes
);