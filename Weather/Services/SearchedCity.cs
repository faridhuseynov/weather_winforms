using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Services
{
    public interface SearchedCity
    {
        void AddCity(City city);
        IEnumerable<City> GetCities();
        IEnumerable<Day> GetDays();
        void SetDays(JObject jObject, int city_index);
    }
}
