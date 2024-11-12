using System.Linq.Expressions;
using AutoMapper;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.Car;
using AutoNaJuz.ViewModels.CarRental;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services;

public class CarsService(
    AppDbContext context,
    IMapper mapper
) : ICarsService
{
    public async Task<int> Create(CreateCarVm car)
    {
        var carEntity = mapper.Map<Car>(car);
        var entry = context.Cars.Add(carEntity);
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task Update(EditCarVm car)
    {
        var id = car.Id;

        var carEntity = await context.Cars.FindAsync(id);
        if (carEntity == null)
            return;

        mapper.Map(car, carEntity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var car = await context.Cars.FindAsync(id);
        if (car == null)
            return;

        context.Cars.Remove(car);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<GetCarVm>> GetAll(Expression<Func<Car, bool>>? predicate = null)
    {
        return (await context.Cars
                .AsNoTracking()
                .Include(c => c.Features)
                .Include(c => c.Rentals)
                .Where(predicate ?? (c => true))
                .Select(e => new
                {
                    e.Id,
                    e.Title,
                    e.Transmission,
                    e.ProductionYear,
                    e.FuelType,
                    e.SeatCount,
                    e.DoorCount,
                    e.BodyType,
                    e.Features,
                    e.Rentals
                })
                .ToListAsync())
            .Select(c => new GetCarVm(
                c.Id,
                c.Title,
                c.Transmission,
                c.ProductionYear,
                c.FuelType,
                c.SeatCount,
                c.DoorCount,
                c.BodyType,
                c.Features.Select(e => new GetCarFeatureVm(e.Id, e.Title)),
                c.Rentals.Select(e => new GetCarRentalVm(e.Id, e.CarId, e.RenterId, e.PerHourCost, e.PerDayCost, e.From, e.To, e.Notes))
            ));
    }

    public async Task<GetCarVm?> GetById(int id)
    {
        return (await GetAll(e => e.Id == id)).FirstOrDefault();
    }
}