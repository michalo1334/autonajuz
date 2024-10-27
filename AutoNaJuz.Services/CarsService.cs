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
        AppDbContext context
    ) : ICarsService
    {
        public async Task<int> Create(Car car)
        {
            var id = context.Cars.Add(car).Entity.Id;
            await context.SaveChangesAsync();
            return id;
        }

        public async Task Update(Car car)
        {
            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Car car)
        {
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAll(Expression<Func<Car, bool>>? predicate = null)
        {
            predicate ??= (car) => true;

            return await context.Cars
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Car?> GetById(int id)
        {
            return await context.Cars.FindAsync(id).AsTask();
        }
    }
}