using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoNaJuz.Model.CarRental;

namespace AutoNaJuz.ViewModels.CarRental
{
    public sealed record CreateOrEditCarRental(
        int CarId,
        string UserId,
        decimal? PerHourCost,
        decimal? PerDayCost,
        DateTime From,
        DateTime To,
        string? Notes
    );
}