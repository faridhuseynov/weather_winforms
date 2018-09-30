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
        void AddCity(JObject jObject);
        IEnumerable<City> GetCities();
        //IEnumerable<Day> GetDays(int city_index);
        void SetDaysInfo(JObject jObject, int city_index);
    }
}
