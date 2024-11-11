using System.Linq.Expressions;
using AutoNaJuz.Model.Car;
using AutoNaJuz.ViewModels.Car;

namespace AutoNaJuz.Services.Interfaces;

public interface ICarsService
{
    Task<IEnumerable<GetCarVm>> GetAll(Expression<Func<Car, bool>>? predicate = null);
    Task<GetCarVm?> GetById(int id);

    Task<int> Create(CreateCarVm car);
    Task Update(EditCarVm car);
    Task Delete(int id);
}