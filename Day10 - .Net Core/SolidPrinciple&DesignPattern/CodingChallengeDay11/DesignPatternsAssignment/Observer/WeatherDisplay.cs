using System;

namespace Observer
{
    public class WeatherDisplay : IObserver
    {
        private string _name;

        public WeatherDisplay(string name)
        {
            _name = name;
        }

        public void Update(float temperature, float humidity)
        {
            Console.WriteLine($"{_name} received update: Temperature={temperature}, Humidity={humidity}");
        }
    }
}
