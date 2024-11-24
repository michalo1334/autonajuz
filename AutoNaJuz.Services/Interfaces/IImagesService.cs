using AutoNaJuz.ViewModels.Image;

namespace AutoNaJuz.Services.Interfaces;

public interface IImagesService
{
    public Task<IEnumerable<ImageVm>> GetAll();

    public Task<ImageVm?> GetById(int id);
    
    public Task<(byte[] blob, string mime)?> GetBlobById(int id);
    
    public Task Delete(int id);

    public Task<int> Upload(byte[] bytes, string mimeType);
}