namespace AutoNaJuz.Model.CarImage;

public class CarImage
{
    public CarImage(
        int id,
        string mimeType,
        byte[] blob,
        string? description = null)
    {
        Id = id;
        MimeType = mimeType;
        Blob = blob;
        Description = description;
    }
    
    public static CarImage Create(
        string mimeType,
        byte[] blob,
        string? description = null)
    {
        return new CarImage(default, mimeType, blob, description);
    }
    
    public void Update(
        string? description = null)
    {
        Description = description;
    }
    
    public int Id { get; set; }
    
    public string MimeType { get; private set; }
    public byte[] Blob { get; private set; }
    
    public string? Description { get; set; }
    
    //Navigation properties
    public IList<Car.Car> Cars { get; set; } = [];
}