using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Services
{
    public class JsonFileSaver : IFileSaver
    {
        public IEnumerable<City> Load()
        {
            var json = File.ReadAllText("database");
            var list = JsonConvert.DeserializeObject<List<City>>(json);
            return list;
        }

        public void Save(IEnumerable<City> SearchedCities)
        {
            var json = JsonConvert.SerializeObject(SearchedCities);
            File.WriteAllText("database", json);
        }
    }
}
