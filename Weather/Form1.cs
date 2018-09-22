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

namespace Weather
{
    public partial class Form1 : Form
    {
        WebClient webClient = new WebClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var chosen_city = textBoxSearch.Text;
            var json = webClient.DownloadString($"http://api.openweathermap.org/data/2.5/forecast?q={chosen_city}&apikey=9542671897d09d6cd3bbd8b596398433&units=metric");
            var result = JsonConvert.DeserializeObject(json) as JObject;
            var city = result["city"]["name"].ToString() +", "+ result["city"]["country"].ToString();
            labelCityResult.Text = city;
        }
        
    }
}
