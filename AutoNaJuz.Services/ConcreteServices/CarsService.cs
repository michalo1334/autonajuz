using System.Linq.Expressions;
using AutoMapper;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.Car;
using AutoNaJuz.ViewModels.Image;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services.ConcreteServices;

public class CarsService(
    AppDbContext context,
    IMapper mapper
) : ICarsService
{
    public async Task<int> Create(CreateCarVm car)
    {
        var idSet = car.ImageIds.ToHashSet();
        
        var carEntity = mapper.Map<Car>(car);
        
        var entry = context.Cars.Add(carEntity);
        entry.Entity.UpdateImages(await context.CarImages.Where(i => idSet.Contains(i.Id)).ToListAsync());
        
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task Update(EditCarVm car)
    {
        var id = car.Id;

        var carEntity = await context.Cars
            .Include(e => e.Images)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (carEntity == null)
            return;
        
        carEntity.Update(
            car.Title,
            car.Transmission,
            car.ProductionYear,
            car.FuelType,
            car.SeatCount,
            car.DoorCount,
            car.BodyType
        );
        carEntity.UpdateImages(await context.CarImages
            .Where(i => car.ImageIds.Contains(i.Id))
            .ToListAsync());
        
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

    public async Task<IEnumerable<ImageVm>> GetImagesByCarId(int id)
    {
        return await context.Cars
            .Include(c => c.Images)
            .Where(c => c.Id == id)
            .SelectMany(c => c.Images)
            .Select(i => new ImageVm(i.Id, i.MimeType))
            .ToListAsync();
    }

    public async Task<IEnumerable<GetCarVm>> GetAll(Expression<Func<Car, bool>>? predicate = null)
    {
        return (await context.Cars
                .AsNoTracking()
                .Include(c => c.Features)
                .Include(c => c.Rentals)
                .Include(c => c.Images)
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
                    e.Rentals,
                    e.Images
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
                c.Images.Select(i => i.Id).ToList()
            ));
    }

    public async Task<GetCarVm?> GetById(int id)
    {
        return (await GetAll(e => e.Id == id)).FirstOrDefault();
    }
}