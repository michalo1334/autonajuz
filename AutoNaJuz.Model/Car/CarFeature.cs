using System.Text.Json.Serialization;

namespace AutoNaJuz.Model.Car;

public class CarFeature
{
    [JsonConstructor]
    public CarFeature(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public static CarFeature Create(string title)
    {
        return new CarFeature(default, title);
    }

    public void Update(string title)
    {
        Title = title;
    }

    public int Id { get; set; }

    public string Title { get; private set; }

    //Navigation properties
    public IList<Car> Cars { get; set; } = [];
}