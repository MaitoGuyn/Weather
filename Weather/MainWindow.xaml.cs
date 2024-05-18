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
using static System.Net.Mime.MediaTypeNames;

namespace Weather
{
    
    public partial class MainWindow : Window
    {
        private List<WeatherForApp> _weather;
        private List<WeatherStatus> _weatherStatus;
        private int temperature;
        private DateTime dateTime;
        public MainWindow()
        {
            InitializeComponent();

            _weather = Data.DataContext.Weather;
            _weatherStatus = Data.DataContext.weatherStatuses;

            CB.ItemsSource = _weatherStatus; 
            CB.DisplayMemberPath = "Status";
            TBW.Text = "";
            FilterCB.ItemsSource = _weatherStatus;
            FilterCB.DisplayMemberPath = "Status";

            SortCB.ItemsSource = Data.DataContext.SortTypes;
            SortCB.DisplayMemberPath = "Title";

            DisplayWeather(_weather);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TX.Text, out temperature))
            {
                MessageBox.Show("Пожалуйста, введите корректное значение температуры.");
                return;
            }


            dateTime = DatePicker.SelectedDate ?? DateTime.Now;

            var selectedStatus = CB.SelectedItem as WeatherStatus;
            if (selectedStatus == null)
            {
                MessageBox.Show("Пожалуйста, выберите статус погоды.");
                return;
            }

            _weather.Add(new WeatherForApp(dateTime, temperature, selectedStatus.Status));
            DisplayWeather(_weather);
            TBW.Text = "";


            foreach (WeatherForApp weather in _weather)
            {
                
                    TBW.Text += weather.ToString()  + "\n";
                
                
            }
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterCB.SelectedItem is WeatherStatus selectedStatus)
            {
                var filteredWeather = _weather.Where(x => x.WeatherStatus == selectedStatus.Status).ToList();
                DisplayWeather(filteredWeather);
            }

        }
        private void DisplayWeather(List<WeatherForApp> weatherList)
        {
            TBW.Text = "";
            foreach (var weather in weatherList)
            {
                TBW.Text += weather.ToString() + "\n";
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
            if (SortCB.SelectedItem is SortType selectedSortType)
            {
                List<WeatherForApp> sortedWeather = _weather;

                switch (selectedSortType.Title)
                {
                    case "С самого ранего":
                        sortedWeather = sortedWeather.OrderBy(weather => weather.dateTime).ToList();
                        break;
                    case "С самого позднего":
                        sortedWeather = sortedWeather.OrderByDescending(weather => weather.dateTime).ToList();
                        break;
                    case "По возрастанию температуры":
                        sortedWeather = sortedWeather.OrderBy(weather => weather.temperature).ToList();
                        break;
                    case "По убыванию температуры":
                        sortedWeather = sortedWeather.OrderByDescending(weather => weather.temperature).ToList();
                        break;
                    default:
                        break;
                }
                DisplayWeather(sortedWeather);
            }
            
        }

        private void UpdateStatistics()
        {
            if (_weather.Count == 0)
            {
                AverageTempTextBlock.Text = "Average Temperature: N/A";
                MaxTempTextBlock.Text = "Max Temperature: N/A";
                MinTempTextBlock.Text = "Min Temperature: N/A";
                AnomaliesTextBlock.Text = "Anomalies: N/A";
                return;
            }

            double averageTemp = _weather.Average(x => x.temperature);
            int maxTemp = _weather.Max(x => x.temperature);
            int minTemp = _weather.Min(x => x.temperature);
            List<string> anomalies = new List<string>();

            for (int i = 1; i < _weather.Count; i++)
            {
                int tempDifference = Math.Abs(_weather[i].temperature - _weather[i - 1].temperature);
                if (tempDifference >= 10)
                {
                    anomalies.Add($"Anomaly on {_weather[i].dateTime}: {tempDifference}°C change");
                }
            }

            AverageTempTextBlock.Text = $"Average Temperature: {averageTemp:F2}°C";
            MaxTempTextBlock.Text = $"Max Temperature: {maxTemp}°C";
            MinTempTextBlock.Text = $"Min Temperature: {minTemp}°C";
            AnomaliesTextBlock.Text = "Anomalies: " + (anomalies.Count > 0 ? string.Join(", ", anomalies) : "None");
        }
    }
}
