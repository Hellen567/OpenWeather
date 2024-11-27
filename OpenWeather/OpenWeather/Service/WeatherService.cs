using OpenWeather.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenWeather.Service
{
    public class WeatherService
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string apiKey = "0d13847c27199c5e81554908b81db30d";

        private readonly HttpClient httpClient;

        public WeatherService()
        {
            httpClient = new HttpClient();
        }

        public async Task<WeatherModel> GetWeather(string cidade)
        {
            var url = $"{BaseUrl}?q={cidade}&appid={apiKey}&units=metric&lang=pt";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var weatherResponse = JsonSerializer.Deserialize<WeatherModel>(json);
                    return weatherResponse;
                }

                throw new Exception($"Erro na API {response.StatusCode}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter clima {ex.Message}");
            }
        }
    }
}
