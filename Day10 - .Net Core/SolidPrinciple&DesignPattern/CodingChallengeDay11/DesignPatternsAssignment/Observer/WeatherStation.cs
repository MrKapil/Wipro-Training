using System.Collections.Generic;

namespace Observer
{
    public class WeatherStation
    {
        private List<IObserver> observers = new List<IObserver>();
        private float temperature;
        private float humidity;

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void SetWeatherData(float temperature, float humidity)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(temperature, humidity);
            }
        }
    }
}
