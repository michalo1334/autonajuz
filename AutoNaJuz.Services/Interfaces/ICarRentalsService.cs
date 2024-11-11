using System.Linq.Expressions;
using AutoNaJuz.Model.CarRental;
using AutoNaJuz.ViewModels.CarRental;

namespace AutoNaJuz.Services.Interfaces;

public interface ICarRentalsService
{
    Task<IEnumerable<GetCarRentalVm>> GetAll(Expression<Func<CarRental, bool>>? predicate = null);
    
    Task<IEnumerable<GetCarRentalVm>> GetAllByCarId(int carId);
    Task<IEnumerable<GetCarRentalVm>> GetAllByRenterId(int renterId);
    Task<GetCarRentalVm?> GetById(int id);
    

    Task<int> Create(CreateCarRentalVm carRental);
    Task Update(EditCarRentalVm carRental);
    Task Delete(int id);
}