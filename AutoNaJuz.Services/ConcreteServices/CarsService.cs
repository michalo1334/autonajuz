using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.Car;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services
{
    public class CarsService(
        AppDbContext context,
        IMapper mapper
    ) : ICarsService
    {
        public async Task<int> Create(CreateOrEditCarVM car)
        {
            var carEntity = mapper.Map<Car>(car);
            var entry = context.Cars.Add(carEntity);
            await context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task Update(int id, CreateOrEditCarVM car)
        {
            var carEntity = await context.Cars.FindAsync(id);
            if(carEntity == null)
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

        public async Task<IEnumerable<GetCarVM>> GetAll(Expression<Func<Car, bool>>? predicate = null)
        {
            return (await context.Cars
                .AsNoTracking()
                .Include(c => c.Features)
                .Include(c => c.Rentals)
                .Where(predicate ?? (c => true))
                .Select(e => new {
                    e.Id,
                    e.Title,
                    e.Transmission,
                    e.ProductionYear,
                    e.FuelType,
                    e.SeatCount,
                    e.DoorCount,
                    e.BodyType,
                    Features = e.Features.Select(f => new { f.Id, f.Title }),
                    Rentals = e.Rentals.Select(r => new { r.Id, r.PerHourCost, r.PerDayCost, r.From, r.To, r.Notes })
                })
                .ToListAsync())
                .Select(c => new GetCarVM(
                    c.Id,
                    c.Title,
                    c.Transmission,
                    c.ProductionYear,
                    c.FuelType,
                    c.SeatCount,
                    c.DoorCount,
                    c.BodyType,
                    c.Features.Select(f => mapper.Map<GetCarFeatureVM>(f)),
                    c.Rentals.Select(r => mapper.Map<GetCarRentalVM>(r))
                ));
        }

        public async Task<GetCarVM?> GetById(int id)
        {
            return (await GetAll(e => e.Id == id)).FirstOrDefault();
                
        }
    }
}