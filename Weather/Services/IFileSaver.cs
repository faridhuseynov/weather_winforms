using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Services
{
    public interface IFileSaver
    {
        void Save(IEnumerable<City> SearchedCities);
        IEnumerable<City> Load();
    }
}
