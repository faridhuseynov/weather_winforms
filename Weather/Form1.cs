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
        int city_index = -1;
        WebClient webClient = new WebClient();
        public Form1()
        {
            InitializeComponent();
        }
        List<City> SearchedCities = new List<City>();
        void SetDaysInfo(JObject result)
        {
            for (int i = 0,j=0; i < Int32.Parse(result["cnt"].ToString())-8; i+=8,++j)
            {
                Model.Day day = new Model.Day();
                day.Date = DateTime.Today.AddSeconds(j*86400);
                day.Temperature= result["list"][i]["main"]["temp"].ToString();
                day.Icon=result["list"][i]["weather"][0]["icon"].ToString();
                day.WeatherDescription= result["list"][i]["weather"][0]["description"].ToString();
                SearchedCities[city_index].days.Insert(0, day);
            }
        }
        void SetWindowDays()
        {
            labelDay1.Text = SearchedCities[city_index].days[0].Date.DayOfWeek.ToString() +", "+ SearchedCities[city_index].days[0].Date.Day.ToString();
            labelTempDay1.Text = SearchedCities[city_index].days[0].Temperature;
            labelWeatherDay1.Text = SearchedCities[city_index].days[0].WeatherDescription;
            pictureBoxDay1.ImageLocation = $"http://openweathermap.org/img/w/{SearchedCities[city_index].days[0].Icon}.png";
        }
        void AddCity(JObject result)
        {
            City city = new City();
            city.Name = result["city"]["name"].ToString();
            city.Country= result["city"]["country"].ToString();
            city.Temperature = result["list"][0]["main"]["temp"].ToString();
            city.WeatherIcon= result["list"][0]["weather"][0]["icon"].ToString();
            city.WeatherDescription = result["list"][0]["weather"][0]["description"].ToString();
            city.Humidity = result["list"][0]["main"]["humidity"].ToString();
            city.Wind = result["list"][0]["wind"]["speed"].ToString();
            city.Barometer = result["list"][0]["main"]["pressure"].ToString();
            SearchedCities.Add(city);
            //city.Name= result["city"]["name"].ToString() + ", " + result["city"]["country"].ToString();
            city_index = SearchedCities.Count - 1;
        }
        void SetWindow(int city_index)
        {
            labelCityResult.Text = SearchedCities[city_index].Name + ", " + SearchedCities[city_index].Country;
            labelBarometerValue.Text = SearchedCities[city_index].Barometer;
            labelDegreeResult.Text = SearchedCities[city_index].Temperature;
            labelWeatherDescription.Text = SearchedCities[city_index].WeatherDescription;
            pictureBoxWeather.ImageLocation = $"http://openweathermap.org/img/w/{SearchedCities[city_index].WeatherIcon}.png";
            labelWindValue.Text = SearchedCities[city_index].Wind;
            labelHumidityValue.Text = SearchedCities[city_index].Humidity;
            SetWindowDays();
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var chosen_city = textBoxSearch.Text;
                var json = webClient.DownloadString($"http://api.openweathermap.org/data/2.5/forecast?q={chosen_city}&apikey=9542671897d09d6cd3bbd8b596398433&units=metric");
                var result = JsonConvert.DeserializeObject(json) as JObject;
                if (result["cod"].ToString() == "404")
                {
                    throw new Exception("City was not found");
                }
                AddCity(result);
                SetDaysInfo(result);
                SetWindow(city_index);
                comboBoxSearchedCities.DataSource = null;
                comboBoxSearchedCities.DataSource = SearchedCities;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
