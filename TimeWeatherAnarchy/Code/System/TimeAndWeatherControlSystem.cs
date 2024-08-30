using Colossal.Serialization.Entities;
using Game;
using Game.Simulation;
using TimeWeatherAnarchy.Code.Settings;

namespace TimeWeatherAnarchy.Code.System
{
    public partial class TimeAndWeatherControlSystem : GameSystemBase
    {
        private ClimateSystem _climateSystem;
        private PlanetarySystem _planetarySystem;
        private float _currentTime;
        private bool _isEditor;
        private bool gameLoaded;

        protected override void OnCreate()
        {
            base.OnCreate();
            _climateSystem = World.GetOrCreateSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetOrCreateSystemManaged<PlanetarySystem>();
        }

        protected override void OnGameLoaded(Context serializationContext)
        {
            base.OnGameLoaded(serializationContext);
            _climateSystem = World.GetOrCreateSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetOrCreateSystemManaged<PlanetarySystem>();
            if (_isEditor) return;
            
            UpdateTime();
            UpdateWeather();
        }
        
        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
   
            _isEditor = mode.IsEditor();
            if (_isEditor) return;
            if (mode.IsGameOrEditor())
            {
                return;
            }

            gameLoaded = true;
            _climateSystem = World.GetOrCreateSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetOrCreateSystemManaged<PlanetarySystem>();
            UpdateTime();
            UpdateWeather();
        }

        public void UpdateWeather()
        {
            _climateSystem = World.GetOrCreateSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetOrCreateSystemManaged<PlanetarySystem>();
            if (_isEditor) return;
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
                case ((int)WeatherOptions.Fall):
                    _climateSystem.currentDate.overrideValue = 0.750f;
                    break;
                case ((int)WeatherOptions.Winter): 
                    _climateSystem.currentDate.overrideValue = 1f;
                    break;
                case ((int)WeatherOptions.Custom):
                    _climateSystem.currentDate.overrideValue = Mod.m_Setting.CurrentWeatherTime;
                    break;
            };
            _climateSystem.temperature.overrideValue = Mod.m_Setting.CurrentTemperature;
            _climateSystem.precipitation.overrideValue = Mod.m_Setting.CurrentPrecipitation;
            _climateSystem.cloudiness.overrideValue = Mod.m_Setting.CurrentClouds;
            _climateSystem.aurora.overrideValue = Mod.m_Setting.CurrentAurora;
            _planetarySystem.dayOfYear = Mod.m_Setting.CurrentDayOfTheYear;
            
            _climateSystem.temperature.overrideState = Mod.m_Setting.EnableCustomTemperature;
            _climateSystem.precipitation.overrideState = Mod.m_Setting.EnableCustomPrecipitation;
            _climateSystem.cloudiness.overrideState = Mod.m_Setting.EnableCustomClouds;
            _climateSystem.aurora.overrideState = Mod.m_Setting.EnableCustomAurora;
            _climateSystem.currentDate.overrideState = Mod.m_Setting.WeatherOption != (int) WeatherOptions.Default;
        }

        public void UpdateTime()
        {
            if (_isEditor) return;
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
        }

        protected override void OnUpdate()
        {
            if (_isEditor) return;
            if (gameLoaded)
            {
                gameLoaded = false;
                UpdateWeather();
                UpdateTime();
            }

            _planetarySystem.time = _currentTime;
        }
    }
}
