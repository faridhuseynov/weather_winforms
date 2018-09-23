using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Model;

namespace Weather
{
    public partial class Form1 : Form
    {
        WebClient webClient = new WebClient();
        public Form1()
        {
            InitializeComponent();
        }
        List<City> SearchedCities = new List<City>();
        void AddCity(JObject result)
        {
            City city = new City();
            city.Name = result["city"]["name"].ToString();
            city.Country= result["city"]["country"].ToString();
            city.Degree = result["list"]["temp"].ToString();
            city.WeatherIcon= result["list"]["weather"]["icon"].ToString();
            city.WeatherDescription = result["list"]["weather"]["description"].ToString();
            city.Humidity = result["list"]["humidity"].ToString();
            city.Wind = result["list"]["wind"]["speed"].ToString();
            city.Barometer = result["list"]["pressure"].ToString();
            SearchedCities.Add(city);
            //city.Name= result["city"]["name"].ToString() + ", " + result["city"]["country"].ToString();
            labelCityResult.Text = city.Name+", "+city.Country;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var chosen_city = textBoxSearch.Text;
                var json = webClient.DownloadString($"http://api.openweathermap.org/data/2.5/forecast?q={chosen_city}&apikey=9542671897d09d6cd3bbd8b596398433&units=metric");
                var result = JsonConvert.DeserializeObject(json) as JObject;
                if (result["message"].ToString()=="404")
                {
                    throw new Exception("City was not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
