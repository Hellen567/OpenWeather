using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenWeather.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenWeather.ViewModel
{
    public partial class WeatherViewModel : ObservableObject
    {
        private readonly WeatherService weatherService;

        public WeatherViewModel()
        {
            weatherService = new WeatherService();
            GetWeatherCommand = new AsyncRelayCommand(GetWeatherAsync);
        }

        [ObservableProperty]
        public string cidade;
        [ObservableProperty]
        public string descricao;
        [ObservableProperty]
        public float temperatura;

        public ICommand GetWeatherCommand { get; set; }

        private async Task GetWeatherAsync()
        {
            var weatherData = await weatherService.GetWeather(Cidade);

            if (weatherData == null)
            {
                Descricao = "Não há dados disponíveis";
                Temperatura = 0;
            }
            else
            {
                Descricao = weatherData.weather[0].description;
                Temperatura = weatherData.main.temp;
            }
        }
    }
}
