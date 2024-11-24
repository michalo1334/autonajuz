namespace AutoNaJuz.ViewModels.CarRental;

public sealed record GetCarRentalVm(
    int Id,
    int CarId,
    int RenterId,
    DateTime From,
    DateTime To,
    string? Notes
);