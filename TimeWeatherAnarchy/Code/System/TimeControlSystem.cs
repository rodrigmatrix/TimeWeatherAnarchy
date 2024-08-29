using Colossal.Serialization.Entities;
using Game;
using Game.Simulation;
using TimeWeatherAnarchy.Code.Settings;

namespace TimeWeatherAnarchy.Code.System
{
    public partial class TimeControlSystem : GameSystemBase
    {
        private ClimateSystem _climateSystem;
        private PlanetarySystem _planetarySystem;
        private float _currentTime;

        protected override void OnCreate()
        {
            base.OnCreate();
            _climateSystem = World.GetExistingSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetExistingSystemManaged<PlanetarySystem>();
            Enabled = true;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            if (!mode.IsGameOrEditor())
                return;

            UpdateTime();
            UpdateWeather();
        }

        public void UpdateWeather()
        {
            _climateSystem.temperature.overrideState = Mod.m_Setting.EnableCustomTemperature;
            _climateSystem.temperature.overrideValue = Mod.m_Setting.CurrentTemperature;
            _climateSystem.currentDate.overrideState = true;
            switch (Mod.m_Setting.WeatherOption)
            {
                case ((int)WeatherOptions.Default):
                    _climateSystem.currentDate.overrideState = false;
                    break;
                case ((int)WeatherOptions.Spring):
                    _climateSystem.currentDate.overrideValue = 0.250f;
                    break;
                case ((int)WeatherOptions.Summer):
                    _climateSystem.currentDate.overrideValue = 0.500f;
                    break;
                case ((int)WeatherOptions.Autumn):
                    _climateSystem.currentDate.overrideValue = 0.750f;
                    break;
                case ((int)WeatherOptions.Winter): 
                    _climateSystem.currentDate.overrideValue = 1f;
                    break;
                case ((int)WeatherOptions.Custom):
                    _climateSystem.currentDate.overrideValue = Mod.m_Setting.CurrentWeatherTime;
                    break;
            };

            _climateSystem.precipitation.overrideValue = Mod.m_Setting.CurrentPrecipitation;
            _climateSystem.precipitation.overrideState = Mod.m_Setting.EnableCustomPrecipitation;

            _climateSystem.cloudiness.overrideValue = Mod.m_Setting.CurrentClouds;
            _climateSystem.cloudiness.overrideState = Mod.m_Setting.EnableCustomClouds;
            
            _climateSystem.aurora.overrideValue = Mod.m_Setting.CurrentAurora;
            _climateSystem.aurora.overrideState = Mod.m_Setting.EnableCustomAurora;
        }

        public void UpdateTime()
        {
            _currentTime = Mod.m_Setting.TimeOption switch
            {
                ((int) TimeOptions.Default) => 12,
                ((int) TimeOptions.Day) => 12,
                ((int) TimeOptions.Night) => 22,
                ((int) TimeOptions.Custom) => Mod.m_Setting.CurrentTime,
                _ => Mod.m_Setting.CurrentTime,
            };
            _planetarySystem.time = _currentTime;
            _planetarySystem.overrideTime = Mod.m_Setting.TimeOption != (int) TimeOptions.Default;

            // do not add to onUpdate
            _planetarySystem.dayOfYear = Mod.m_Setting.CurrentDayOfTheYear;
        }

        protected override void OnUpdate()
        {
            _planetarySystem.time = _currentTime;
        }
    }
}
