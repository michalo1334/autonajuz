using System.Linq.Expressions;
using AutoNaJuz.Model;
using AutoNaJuz.Model.RenterInfo;
using AutoNaJuz.ViewModels.RenterInfo;

namespace AutoNaJuz.Services.Interfaces;

public interface IRenterInfoService
{
    Task<IEnumerable<GetRenterInfoVm>> GetAll(Expression<Func<RenterInfo, bool>>? predicate = null);
    Task<GetRenterInfoVm?> GetById(int id);

    Task<int> Create(CreateRenterInfoVm renterInfo);
    Task Update(EditRenterInfoVm renterInfo);
    Task Delete(int id);
}