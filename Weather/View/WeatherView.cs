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
using Weather.Presenter;
using Weather.Services;

namespace Weather
{
    public partial class WeatherView : Form
    {
        public WeatherPresenter Presenter { get; set; }
        WebClient webClient = new WebClient();
        public WeatherView(WeatherPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;      
        }
        public void UpdateDays(IEnumerable<City> SearchedCities, int city_index)
        {
            //searched day result
            var list = SearchedCities.ToList();
            labelCityResult.Text = list[city_index].Name + ", " + list[city_index].Country;
            labelBarometerValue.Text = list[city_index].Barometer;
            labelDegreeResult.Text = list[city_index].Temperature;
            labelWeatherDescription.Text = list[city_index].WeatherDescription;
            pictureBoxWeather.ImageLocation = $"http://openweathermap.org/img/w/{list[city_index].WeatherIcon}.png";
            labelWindValue.Text = list[city_index].Wind;
            labelHumidityValue.Text = list[city_index].Humidity;
            //day1
            labelDay1.Text = list[city_index].days[0].Date.DayOfWeek.ToString() +", "+ list[city_index].days[0].Date.Day.ToString();
            labelTempDay1.Text = list[city_index].days[0].Temperature;
            labelWeatherDay1.Text = list[city_index].days[0].WeatherDescription;
            pictureBoxDay1.ImageLocation = $"http://openweathermap.org/img/w/{list[city_index].days[0].Icon}.png";
            //day2
            labelDay2.Text = list[city_index].days[1].Date.DayOfWeek.ToString() + ", " + list[city_index].days[1].Date.Day.ToString();
            labelTempDay2.Text = list[city_index].days[1].Temperature;
            labelWeatherDay2.Text = list[city_index].days[1].WeatherDescription;
            pictureBoxDay2.ImageLocation = $"http://openweathermap.org/img/w/{list[city_index].days[1].Icon}.png";
            //day3
            labelDay3.Text = list[city_index].days[2].Date.DayOfWeek.ToString() + ", " + list[city_index].days[2].Date.Day.ToString();
            labelTempDay3.Text = list[city_index].days[2].Temperature;
            labelWeatherDay3.Text = list[city_index].days[2].WeatherDescription;
            pictureBoxDay3.ImageLocation = $"http://openweathermap.org/img/w/{list[city_index].days[2].Icon}.png";
            //day4
            labelDay4.Text = list[city_index].days[3].Date.DayOfWeek.ToString() + ", " + list[city_index].days[3].Date.Day.ToString();
            labelTempDay4.Text = list[city_index].days[3].Temperature;
            labelWeatherDay4.Text = list[city_index].days[3].WeatherDescription;
            pictureBoxDay4.ImageLocation = $"http://openweathermap.org/img/w/{list[city_index].days[3].Icon}.png";
            //day5
            labelDay5.Text = list[city_index].days[4].Date.DayOfWeek.ToString() + ", " + list[city_index].days[4].Date.Day.ToString();
            labelTempDay5.Text = list[city_index].days[4].Temperature;
            labelWeatherDay5.Text = list[city_index].days[4].WeatherDescription;
            pictureBoxDay5.ImageLocation = $"http://openweathermap.org/img/w/{list[city_index].days[4].Icon}.png";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var chosen_city = textBoxSearch.Text;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(chosen_city[0].ToString().ToUpper());
            for (int i = 1; i < chosen_city.Count(); i++)
            {
                stringBuilder.Append(chosen_city[i]);
            }
            var searched_city = stringBuilder.ToString();
            if (Presenter.CheckCity(searched_city) == true)
                comboBoxSearchedCities.SelectedIndex = Presenter.city_index;
            else
            {
                try
                {
                    var json = webClient.DownloadString($"http://api.openweathermap.org/data/2.5/forecast?q={chosen_city}&apikey=9542671897d09d6cd3bbd8b596398433&units=metric");
                    var result = JsonConvert.DeserializeObject(json) as JObject;
                    Presenter.AddCity(result);
                    comboBoxSearchedCities.DataSource = null;
                    comboBoxSearchedCities.DataSource = Presenter.Storage.GetCities();
                    int index = comboBoxSearchedCities.FindString(Presenter.Storage.SearchedCities[Presenter.Storage.SearchedCities.Count - 1].ToString());
                    comboBoxSearchedCities.SelectedIndex = index;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxSearchedCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxSearchedCities.SelectedIndex;
            if (index < 0)
                return;
            string chosen_city = Presenter.Storage.SearchedCities[index].Name.ToString();
            Presenter.CheckCity(chosen_city);
        }

        private void WeatherView_Load(object sender, EventArgs e)
        {
            Presenter.Storage.SearchedCities = Presenter.Saver.Load().ToList();
            comboBoxSearchedCities.DataSource = Presenter.Storage.SearchedCities;
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            Presenter.Saver.Save(Presenter.Storage.SearchedCities);
        }

    }
}
