using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Presenter;

namespace Weather
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var weatherPresenter = new WeatherPresenter();
            var weatherView = new WeatherView(weatherPresenter);
            weatherPresenter.View = weatherView;
            Application.Run(weatherView);
        }
    }
}
