using System.Linq.Expressions;
using AutoNaJuz.Model.Car;
using AutoNaJuz.ViewModels.Car;
using AutoNaJuz.ViewModels.Image;

namespace AutoNaJuz.Services.Interfaces;

public interface ICarsService
{
    Task<IEnumerable<GetCarVm>> GetAll(Expression<Func<Car, bool>>? predicate = null);
    Task<IEnumerable<GetCarVm>> GetAll(string? search, bool? onlyAvailable, DateTime? availableFrom, DateTime? availableTo);
    
    Task<GetCarVm?> GetById(int id);

    Task<int> Create(CreateCarVm car);
    Task Update(EditCarVm car);
    Task Delete(int id);
    Task<IEnumerable<ImageVm>> GetImagesByCarId(int id);

    Task<IEnumerable<CarFeatureVm>> GetAllFeatures();
    Task<CarFeatureVm?> GetFeatureById(int id);
    Task<int> CreateFeature(string title);
    Task UpdateFeature(int id, string title);
    Task DeleteFeature(int id);
}