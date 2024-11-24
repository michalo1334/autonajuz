namespace AutoNaJuz.ViewModels.CarRental;

public record CreateCarRentalVm(
    int CarId,
    int RenterId,
    DateTime From,
    DateTime To,
    string? Notes
);