using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Services
{
    public class ISearchedCitiesStorage : SearchedCity
    {
        List<City> SearchedCities = new List<City>();
        List<Day> Days = new List<Day>();
        public void AddCity(City city)
        {
            SearchedCities.Add(city);
        }

        public IEnumerable<City> GetCities()
        {
            return SearchedCities;
        }

        public IEnumerable<Day> GetDays()
        {
            return Days;
        }

        void SearchedCity.SetDays( JObject jObject, int city_index)
        {
            for (int i = 0, j = 0; i <= Int32.Parse(jObject["cnt"].ToString()) - 8; i += 8, ++j)
            {
                Model.Day day = new Model.Day();
                day.Date = DateTime.Today.AddSeconds(j * 86400);
                day.Temperature = jObject["list"][i]["main"]["temp"].ToString();
                day.Icon = jObject["list"][i]["weather"][0]["icon"].ToString();
                day.WeatherDescription = jObject["list"][i]["weather"][0]["description"].ToString();
                SearchedCities[city_index].days.Insert(0, day);
            }
            SearchedCities[city_index].days.Reverse();
        }
    }
}
