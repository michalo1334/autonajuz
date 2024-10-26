using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model;
using AutoNaJuz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services
{
    public class CarsService(
        ICarRentalsService carRentalsService,
        AppDbContext context
    ) : ICarsService
    {
        public async Task Create(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            context.Cars.Remove(new Car { Id = id });
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAll(Expression<Func<Car, bool>>? predicate) => predicate == null
                ? await context.Cars.ToListAsync()
                : await context.Cars.Where(predicate).ToListAsync();

        public async Task<Car?> GetById(int id)
        {
            return await context.Cars.FindAsync(id);
        }

        public async Task<bool> IsAvailableForRentNow(int id)
        {
            return await context.Cars
                .Include(c => c.Rentals)
                .Where(c => c.Id == id)
                .AllAsync(c => c.Rentals.All(cr => cr.To < DateTime.Now));
        }

        public Task<int> Rent(int carId, int userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}