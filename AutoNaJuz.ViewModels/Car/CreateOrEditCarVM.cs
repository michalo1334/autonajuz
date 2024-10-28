using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoNaJuz.Model.Car;

namespace AutoNaJuz.ViewModels.Car
{
    public sealed record CreateOrEditCarVM(
        string Title,
        TransmissionType Transmission,
        DateTime ProductionYear,
        FuelType FuelType,
        int SeatCount,
        int DoorCount,
        CarBodyType BodyType
    );
}