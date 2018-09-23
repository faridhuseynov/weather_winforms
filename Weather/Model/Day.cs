using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model
{
    public class Day
    {
        public DateTime Date { get; set; } = new DateTime();
        public string DayofWeek { get; set; }
        public string Icon { get; set; }
        public string Temperature { get; set; }
        public string WeatherDescription { get; set; }
    }
}
