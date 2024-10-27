using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoNaJuz.Model.Car
{
    public class CarFeature
    {
        [JsonConstructor]
        public CarFeature(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public static CarFeature Create(int id, string title)
        {
            return new CarFeature(id, title);
        }

        public void Update(string title)
        {
            Title = title;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        //Navigation properties
        public IList<Car> Cars { get; set; } = [];
    }
}