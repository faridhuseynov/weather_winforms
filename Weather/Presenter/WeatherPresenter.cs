﻿using Newtonsoft.Json.Linq;
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
        public ISearchedCitiesStorage Storage { get; set; }
        public void AddCity(JObject jObject)
        {
            Storage.AddCity(jObject);
            city_index = Storage.SearchedCities.Count-1;
            View.UpdateDays(Storage.GetCities(),city_index);
        }
 
    }
}