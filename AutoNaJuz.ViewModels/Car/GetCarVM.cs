using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoNaJuz.Model.Car;

namespace AutoNaJuz.ViewModels.Car
{
    public sealed record GetCarVM(
        int Id,
        string Title,
        TransmissionType Transmission,
        DateTime ProductionYear,
        FuelType FuelType,
        int SeatCount,
        int DoorCount,
        CarBodyType BodyType,
        IEnumerable<GetCarFeatureVM> Features,
        IEnumerable<GetCarRentalVM> Rentals,
        GetCarRentalVM? CurrentRental
    );
}