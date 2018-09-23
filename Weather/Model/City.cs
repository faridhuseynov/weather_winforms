using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model
{
    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Temperature { get; set; }
        public string WeatherIcon { get; set; }
        public string WeatherDescription { get; set; }
        public string Barometer { get; set; }
        public string Humidity { get; set; }
        public string Wind { get; set; }
        public List<Day> days = new List<Day>();
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
