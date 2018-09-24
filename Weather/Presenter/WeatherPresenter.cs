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
        public WeatherView View { get; set; }
        public ISearchedCitiesStorage Storage { get; set; }
        public void AddCity(City city)
        {
            Storage.AddCity(city);
            //ViewUpdateWeather to be written

        }
    }
}
