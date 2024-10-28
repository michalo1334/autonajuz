using System.Linq.Expressions;
using AutoNaJuz.Model.Car;
using AutoNaJuz.ViewModels.Car;

namespace AutoNaJuz.Services.Interfaces
{
    public interface ICarsService
    {
        Task<IEnumerable<GetCarVM>> GetAll(Expression<Func<Car, bool>>? predicate = null);
        Task<GetCarVM?> GetById(int id);

        Task<int> Create(CreateOrEditCarVM car);
        Task Update(int id, CreateOrEditCarVM car);
        Task Delete(int id);
    }
}