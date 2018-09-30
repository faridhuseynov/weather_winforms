using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;
using Weather.Services;

namespace Weather.Presenter
{
    public class WeatherPresenter
    {
        public int city_index = -1;
        public WeatherView View { get; set; }
        public ISearchedCitiesStorage Storage { get; set; } = new ISearchedCitiesStorage();
        public JsonFileSaver Saver = new JsonFileSaver();
        public void AddCity(JObject jObject)
        {
            Storage.AddCity(jObject);
            city_index = Storage.SearchedCities.Count-1;
        }
        public bool CheckCity (string city){
            int index = 0;
            foreach (var item in Storage.SearchedCities)
            {
                if (item.Name == city)
                {
                    View.UpdateDays(Storage.GetCities(), index);
                    city_index = index;
                    return true;
                }
                ++index;
            }
            return false;
        }
        public void SaveCityList()
        {
            Saver.Save(Storage.SearchedCities);
        }
        public void LoadCityList()
        {
            Saver.Load();
        }
    }
}
