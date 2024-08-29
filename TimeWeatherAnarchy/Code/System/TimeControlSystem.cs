using System.Threading.Tasks;
using Colossal.Serialization.Entities;
using Game;
using Game.Rendering;
using Game.Simulation;
using TimeWeatherAnarchy.Code.Settings;
using UnityEngine;

namespace TimeWeatherAnarchy.Code.System
{
    public partial class TimeControlSystem : GameSystemBase
    {
        public ClimateSystem _climateSystem;
        public PlanetarySystem _planetarySystem;
        public LightingSystem _lightingSystem;
        public bool IsInitialized;
        private bool overrideTime = false;
        private float currentOverrideTime = 0f;

        protected override void OnCreate()
        {
            base.OnCreate();
            _lightingSystem = World.GetExistingSystemManaged<LightingSystem>();
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
            IsInitialized = true;
        }

        public void UpdateWeather()
        {
            _climateSystem.temperature.overrideValue = Mod.m_Setting.CurrentTemperature;
            _climateSystem.temperature.overrideState = Mod.m_Setting.WeatherOption == (int) WeatherOptions.Custom;

            _climateSystem.precipitation.overrideValue = 1;
            _climateSystem.precipitation.overrideState = true;

            _climateSystem.cloudiness.overrideValue = 1;
            _climateSystem.cloudiness.overrideState = true;
        }

        public void UpdateTime()
        {
            overrideTime = Mod.m_Setting.TimeOption == (int) TimeOptions.Custom;
            currentOverrideTime = Mod.m_Setting.CurrentTime;
            _planetarySystem.time = currentOverrideTime;

            // do not add to onUpdate
            _planetarySystem.dayOfYear = 215;
        }

        protected override void OnUpdate()
        {
            _planetarySystem.overrideTime = overrideTime;
            _planetarySystem.time = currentOverrideTime;
        }
    }
}
