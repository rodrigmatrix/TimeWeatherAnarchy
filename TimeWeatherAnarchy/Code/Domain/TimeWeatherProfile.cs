using System;

namespace TimeWeatherAnarchy.Code.Domain
{
    public class TimeWeatherProfile
    {
        public const string DefaultID = "default_profile";
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public int Index { get; set; }
        
        public float Time { get; set; }
        
        public float WeatherTime { get; set; }
        
        public int Temperature { get; set; }
        
        public float Fog { get; set; }
        
        public float Thunder { get; set; }
        
        public int TimeOption { get; set; }
        
        public int WeatherOption { get; set; }
        
        public bool EnableCustomPrecipitation { get; set; }
        
        public bool EnableCustomTemperature { get; set; }
        
        public bool EnableCustomClouds { get; set; }
        
        public bool EnableCustomAurora { get; set; }
        
        public bool EnableCustomFog { get; set; }
        
        public bool EnableCustomThunder { get; set; }
        
        public float Rainbow { get; set; }
        
        public float Aurora { get; set; }
        
        public float Clouds { get; set; }
        
        public float Precipitation { get; set; }
        
        public int DayOfTheYear { get; set; }
        
        public float Latitude { get; set; } = 45.0f;

        public float Longitude { get; set; } = 90.0f;

        public static TimeWeatherProfile Create(string name, int index) => new(Guid.NewGuid(), index, name);

        public TimeWeatherProfile()
        {
            
        }
        
        private TimeWeatherProfile(Guid id, int index, string name)
        {
            Id = id.ToString();
            Index = index;
            Name = name;
        }
    }
}