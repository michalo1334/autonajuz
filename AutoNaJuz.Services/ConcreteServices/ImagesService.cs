using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.CarImage;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.Image;
using Microsoft.EntityFrameworkCore;

namespace AutoNaJuz.Services.ConcreteServices;

public class ImagesService(
    AppDbContext context) : IImagesService
{
    public async Task<IEnumerable<ImageVm>> GetAll() =>
        await context.CarImages
            .Select(e => new ImageVm(e.Id, e.MimeType))
            .ToListAsync();
    
    public async Task<IEnumerable<ImageVm>> GetByIds(IList<int> ids)
    {
        var imageIdsSet = ids.ToHashSet();
        
        return await context.CarImages
            .Where(e => imageIdsSet.Contains(e.Id))
            .Select(e => new ImageVm(e.Id, e.MimeType))
            .ToListAsync();
    }

    public async Task<IEnumerable<ImageVm>> GetByCarId(int carId)
    {
        var car = context.Cars
            .Include(e => e.Images)
            .FirstOrDefault(e => e.Id == carId);
        
        if (car == null)
            return [];
        
        var imageIdsSet = car.Images.Select(i => i.Id).ToHashSet();

        return await context.CarImages
            .Where(e => imageIdsSet.Contains(e.Id))
            .Select(e => new ImageVm(e.Id, e.MimeType))
            .ToListAsync();
    }

    public async Task<ImageVm?> GetById(int id) =>
        await context.CarImages
            .Where(e => e.Id == id)
            .Select(e => new ImageVm(e.Id, e.MimeType))
            .FirstOrDefaultAsync();

    public async Task<(byte[] blob, string mime)?> GetBlobById(int id)
    {
        var image = await context.CarImages.FindAsync(id);
        if (image == null)
            return null;
        
        return (image.Blob, image.MimeType);
    }

    public async Task Delete(int id)
    {
        var image = await context.CarImages.FindAsync(id);
        if (image == null)
            return;
        
        context.CarImages.Remove(image);
        await context.SaveChangesAsync();
    }

    public async Task<int> Upload(byte[] bytes, string mimeType)
    {
        var image = CarImage.Create(mimeType, bytes);
        
        var entry = context.CarImages.Add(image);
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }
}