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
        entry.Entity.UpdateImages(await context.CarImages
            .Where(i => idSet.Contains(i.Id))
            .ToListAsync());
        entry.Entity.UpdateFeatures(await context.CarFeatures
            .Where(f => car.FeatureIds.Contains(f.Id))
            .ToListAsync());
        
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task Update(EditCarVm car)
    {
        var id = car.Id;

        var carEntity = await context.Cars
            .Include(e => e.Images)
            .Include(e => e.Features)
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
            car.BodyType,
            car.RentCostPerDay
        );
        carEntity.UpdateImages(await context.CarImages
            .Where(i => car.ImageIds.Contains(i.Id))
            .ToListAsync());
        carEntity.UpdateFeatures(await context.CarFeatures
            .Where(f => car.FeatureIds.Contains(f.Id))
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

    public async Task<IEnumerable<CarFeatureVm>> GetAllFeatures() =>
        await context.CarFeatures
            .Select(f => new CarFeatureVm(f.Id, f.Title))
            .ToListAsync();

    public async Task<CarFeatureVm?> GetFeatureById(int id)
    {
        return await context.CarFeatures
            .Where(f => f.Id == id)
            .Select(f => new CarFeatureVm(f.Id, f.Title))
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateFeature(string title)
    {
        var entity = CarFeature.Create(title);
        
        var entry = context.CarFeatures.Add(entity);
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task UpdateFeature(int id, string title)
    {
        var entity = await context.CarFeatures.FindAsync(id);
        if (entity == null)
            return;
        
        entity.Update(title);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFeature(int id)
    {
        var entity = await context.CarFeatures.FindAsync(id);
        if (entity == null)
            return;
        
        context.CarFeatures.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<GetCarVm>> GetAll(Expression<Func<Car, bool>>? predicate = null)
    {
        return (await context.Cars
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
                    e.RentCostPerDay,
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
                c.RentCostPerDay,
                c.Images.Select(i => i.Id).ToList(),
                c.Features.Select(f => new CarFeatureVm(f.Id, f.Title)).ToList()
            ));
    }
    
    public async Task<IEnumerable<GetCarVm>> GetAll(string? search, bool? onlyAvailable, DateTime? availableFrom, DateTime? availableTo)
    {
        var query = context.Cars
            .Include(c => c.Features)
            .Include(c => c.Rentals)
            .Include(c => c.Images)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(search)) 
            query = query.Where(c => c.Title.Contains(search));
        
        if (onlyAvailable == true)
            query = query.Where(c => c.Rentals.All(r => r.To < availableFrom || r.From > availableTo));
        
        return (await query
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
                    e.RentCostPerDay,
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
                c.RentCostPerDay,
                c.Images.Select(i => i.Id).ToList(),
                c.Features.Select(f => new CarFeatureVm(f.Id, f.Title)).ToList()
            ));
    }

    public async Task<GetCarVm?> GetById(int id)
    {
        return (await GetAll(e => e.Id == id)).FirstOrDefault();
    }
}