using AutoNaJuz.Model.CarRental;

namespace AutoNaJuz.ViewModels.Car
{
    public sealed record GetCarRentalVM(
        int Id,
        decimal? PerHourCost,
        decimal? PerDayCost,
        DateTime From,
        DateTime To,
        string? Notes
    );
}