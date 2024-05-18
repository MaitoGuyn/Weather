using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Weather.Data;

namespace Weather
{
    
    public partial class MainWindow : Window
    {
        private List<WeatherForApp> _weather;
        private int temperature;
        private DateTime dateTime;
        public MainWindow()
        {
            InitializeComponent();

            _weather = Data.DataContext.Weather;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            temperature = Convert.ToInt32(TX.Text);
            dateTime = Convert.ToDateTime(TX1.Text);
            _weather.Add(new WeatherForApp(DateTime.Now, temperature));
            TBW.Text = "";
            foreach (WeatherForApp weather in _weather)
            {
                TBW.Text += weather.ToString() + "\n";
            }
            
        }


    }
}
