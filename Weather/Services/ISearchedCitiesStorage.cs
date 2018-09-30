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
        public List<City> SearchedCities = new List<City>();
        public List<Day> Days = new List<Day>();
        public void AddCity(JObject jObject)
        {
            City city = new City();
            city.Name = jObject["city"]["name"].ToString();
            city.Country = jObject["city"]["country"].ToString();
            city.Temperature = jObject["list"][0]["main"]["temp"].ToString();
            city.WeatherIcon = jObject["list"][0]["weather"][0]["icon"].ToString();
            city.WeatherDescription = jObject["list"][0]["weather"][0]["description"].ToString();
            city.Humidity = jObject["list"][0]["main"]["humidity"].ToString();
            city.Wind = jObject["list"][0]["wind"]["speed"].ToString();
            city.Barometer = jObject["list"][0]["main"]["pressure"].ToString();
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

        void SearchedCity.SetDaysInfo( JObject jObject, int city_index)
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
