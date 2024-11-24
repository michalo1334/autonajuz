using System.Linq.Expressions;
using AutoMapper;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.CarRental;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.CarRental;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services.ConcreteServices;

public class CarRentalsService(
    AppDbContext context,
    IMapper mapper
) : ICarRentalsService
{
    public async Task<int> Create(CreateCarRentalVm carRental)
    {
        var carRentalEntity = mapper.Map<CarRental>(carRental);
        var entry = context.CarRentals.Add(carRentalEntity);
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task Update(EditCarRentalVm carRental)
    {
        var id = carRental.Id;

        var carRentalEntity = await context.CarRentals.FindAsync(id);
        if (carRentalEntity == null)
            return;

        mapper.Map(carRental, carRentalEntity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var carRental = await context.CarRentals.FindAsync(id);
        if (carRental == null)
            return;

        context.CarRentals.Remove(carRental);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<GetCarRentalVm>> GetAll(Expression<Func<CarRental, bool>>? predicate = null)
    {
        return (await context.CarRentals
                .Where(predicate ?? (c => true))
                .Select(e => new
                {
                    e.Id,
                    e.CarId,
                    e.RenterId,
                    e.From,
                    e.To,
                    e.Notes
                })
                .ToListAsync())
            .Select(c => new GetCarRentalVm(
                c.Id,
                c.CarId,
                c.RenterId,
                c.From,
                c.To,
                c.Notes
            ));
    }
    
    public async Task<IEnumerable<GetCarRentalVm>> GetAllByCarId(int carId)
    {
        return await GetAll(e => e.CarId == carId);
    }

    public async Task<IEnumerable<GetCarRentalVm>> GetAllByRenterId(int renterId)
    {
        return await GetAll(e => e.RenterId == renterId);
    }

    public async Task<GetCarRentalVm?> GetById(int id)
    {
        return (await GetAll(e => e.Id == id)).FirstOrDefault();
    }
}