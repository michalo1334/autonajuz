using System.Linq.Expressions;
using AutoMapper;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.RenterInfo;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services.ConcreteServices;

public class RenterInfoService(
    AppDbContext context,
    IMapper mapper) : IRenterInfoService
{
    public async Task<IEnumerable<GetRenterInfoVm>> GetAll(Expression<Func<RenterInfo, bool>>? predicate = null)
    {
        return (await context.RenterInfos
                .Where(predicate ?? (c => true))
                .Select(e => new
                {
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.BirthDate,
                    e.DriversLicenseIdent,
                    e.Pesel,
                    e.Street,
                    e.BuildingNumber,
                    e.ApartmentNumber,
                    e.PostalCode,
                    e.City,
                })
                .ToListAsync())
                .Select(e => new GetRenterInfoVm(
                    e.Id,
                    e.DriversLicenseIdent,
                    e.Pesel,
                    e.BirthDate,
                    e.FirstName,
                    e.LastName,
                    e.Street,
                    e.BuildingNumber,
                    e.ApartmentNumber,
                    e.City,
                    e.PostalCode
                ));
    }

    public async Task<GetRenterInfoVm?> GetById(int id)
    {
        return (await GetAll(c => c.Id == id)).FirstOrDefault();
    }

    public async Task<int> Create(CreateRenterInfoVm renterInfo)
    {
        var renterInfoEntity = mapper.Map<RenterInfo>(renterInfo);
        var entry = context.RenterInfos.Add(renterInfoEntity);
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task Update(EditRenterInfoVm renterInfo)
    {
        var id = renterInfo.Id;
        
        var renterInfoEntity = await context.RenterInfos.FindAsync(id);
        if (renterInfoEntity == null)
            return;
        
        mapper.Map(renterInfo, renterInfoEntity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var renterInfo = context.RenterInfos.Find(id);
        if (renterInfo == null)
            return;
        
        context.RenterInfos.Remove(renterInfo);
        await context.SaveChangesAsync();
    }
}