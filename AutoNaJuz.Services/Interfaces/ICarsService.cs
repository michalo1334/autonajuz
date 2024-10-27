using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoNaJuz.Model;

namespace AutoNaJuz.Services.Interfaces
{
    public interface ICarsService
    {
        Task<IEnumerable<Car>> GetAll(Expression<Func<Car, bool>>? predicate = null);
        Task<Car?> GetById(int id);

        Task<int> Create(Car car);
        Task Update(Car car);
        Task Delete(Car car);
    }
}