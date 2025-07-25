﻿using System;
using Colossal.Serialization.Entities;
using Game;
using Game.Simulation;
using TimeWeatherAnarchy.Code.Domain;
using Unity.Entities;

namespace TimeWeatherAnarchy.Code.System
{
    public partial class TimeAndWeatherControlSystem : GameSystemBase
    {
        private ClimateSystem _climateSystem;
        private PlanetarySystem _planetarySystem;
        private SimulationSystem _simulationSystem;
        private float _currentTime;
        private bool _isEditor;
        private bool _gameLoaded;
        private float _time;
        private readonly float _timeDelay = 0.2f;
        private bool _seasonSet;
        private bool _isPaused;

        private const string SPRING_SEASON = "Climate.SEASON[Spring]";
        private const string SUMMER_SEASON = "Climate.SEASON[Summer]";
        private const string FALL_SEASON = "Climate.SEASON[Autumn]";
        private const string WINTER_SEASON = "Climate.SEASON[Winter]";

        protected override void OnCreate()
        {
            base.OnCreate();
            _climateSystem = World.GetOrCreateSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetOrCreateSystemManaged<PlanetarySystem>();
            _simulationSystem = World.GetOrCreateSystemManaged<SimulationSystem>();
        }

        protected override void OnGameLoaded(Context serializationContext)
        {
            base.OnGameLoaded(serializationContext);
            if (_isEditor) return;
            _gameLoaded = true;
            UpdateTimeAndWeather();
        }
        
        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
   
            _isEditor = mode.IsEditor();
            if (_isEditor)
            {
                return;
            }
            _gameLoaded = true;
            UpdateTimeAndWeather();
        }

        public void UpdateTimeAndWeather()
        {
            try
            {
                UpdateWeather();
                UpdateTime();
            }
            catch (Exception e)
            {
                Mod.log.Info(e + " error occured while updating time and weather.");
            }
        }

        private void UpdateWeather()
        {
            if (_isEditor) return;
            UpdateAurora();
            UpdateTemperature();
            UpdateClouds();
            UpdatePrecipitation();
            UpdateSeason();
            //_climateSystem.fog.overrideValue = Mod.m_Setting.Fog;
            //_climateSystem.thunder.overrideValue = Mod.m_Setting.Thunder;
            //_climateSystem.rainbow = Mod.m_Setting.Rainbow;
            //_climateSystem.fog.overrideState = Mod.m_Setting.EnableCustomFog;
            //_climateSystem.thunder.overrideState = Mod.m_Setting.EnableCustomThunder;
        }

        private void UpdateLatitude()
        {
            _planetarySystem.latitude = Mod.m_Setting.Profile.Latitude;
        }

        private void UpdateLongitude()
        {
            _planetarySystem.longitude = Mod.m_Setting.Profile.Longitude;
        }
        
        public void UpdateAurora()
        {
            _climateSystem.aurora.overrideValue = Mod.m_Setting.Profile.Aurora;
            _climateSystem.aurora.overrideState = Mod.m_Setting.Profile.EnableCustomAurora;
        }
        
        public void UpdateTemperature()
        {
            _climateSystem.temperature.overrideValue = Mod.m_Setting.Profile.Temperature;
            _climateSystem.temperature.overrideState = Mod.m_Setting.Profile.EnableCustomTemperature;
        }
        
        public void UpdateClouds()
        {
            _climateSystem.cloudiness.overrideValue = Mod.m_Setting.Profile.Clouds;
            _climateSystem.cloudiness.overrideState = Mod.m_Setting.Profile.EnableCustomClouds;
        }


        public void UpdatePrecipitation()
        {
            _climateSystem.precipitation.overrideValue = Mod.m_Setting.Profile.Precipitation;
            _climateSystem.precipitation.overrideState = Mod.m_Setting.Profile.EnableCustomPrecipitation;
        }


        public void UpdateSeason()
        {
            SetSeason();
            _climateSystem.currentDate.overrideState = Mod.m_Setting.Profile.WeatherOption != (int) WeatherOptions.Default;
            _seasonSet = false;
            _time = 0f;
        }
        
        
        private bool CheckIfInvertSeason()
        {
            var invertSeason = false;
            switch (Mod.m_Setting.Profile.WeatherOption)
            {
                case ((int)WeatherOptions.Default):
                    invertSeason = false;
                    break;
                case ((int)WeatherOptions.Spring):
                    if (_climateSystem.currentSeasonNameID != SPRING_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Summer):
                    if (_climateSystem.currentSeasonNameID != SUMMER_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Fall):
                    if (_climateSystem.currentSeasonNameID != FALL_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Winter): 
                    if (_climateSystem.currentSeasonNameID != WINTER_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Custom):
                    invertSeason = false;
                    break;
            };
            return invertSeason;
        }

        private void SetSeason()
        {
            switch (Mod.m_Setting.Profile.WeatherOption)
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
                    _climateSystem.currentDate.overrideValue = Mod.m_Setting.Profile.WeatherTime;
                    break;
            };
        }

        private void SetInvertedSeason()
        {
            switch (Mod.m_Setting.Profile.WeatherOption)
            {
                case ((int)WeatherOptions.Default):
                    _climateSystem.currentDate.overrideState = false;
                    break;
                case ((int)WeatherOptions.Spring):
                    _climateSystem.currentDate.overrideValue = 0.750f;
                    break;
                case ((int)WeatherOptions.Summer):
                    _climateSystem.currentDate.overrideValue = 1f;
                    break;
                case ((int)WeatherOptions.Fall):
                    _climateSystem.currentDate.overrideValue = 0.250f;
                    break;
                case ((int)WeatherOptions.Winter): 
                    _climateSystem.currentDate.overrideValue = 0.500f;
                    break;
                case ((int)WeatherOptions.Custom):
                    _climateSystem.currentDate.overrideValue = Mod.m_Setting.Profile.WeatherTime;
                    break;
            };
        }

        public void UpdateTime()
        {
            if (_isEditor) return;
            _planetarySystem.overrideTime = Mod.m_Setting.Profile.TimeOption != (int) TimeOptions.Default;
            _planetarySystem.dayOfYear = Mod.m_Setting.Profile.DayOfTheYear;
            //UpdateLatitude();
            //UpdateLongitude();
            _currentTime = Mod.m_Setting.Profile.TimeOption switch
            {
                ((int) TimeOptions.Default) => 0,
                ((int) TimeOptions.Day) => 12,
                ((int) TimeOptions.Night) => 1,
                ((int) TimeOptions.Custom) => Mod.m_Setting.Profile.Time,
                _ => Mod.m_Setting.Profile.Time,
            };
            _planetarySystem.time = _currentTime;
        }

        protected override void OnUpdate()
        {
            var currentIsPaused = _simulationSystem.selectedSpeed == 0;
            if (currentIsPaused != _isPaused)
            {
                _seasonSet = false;
            }
            _isPaused = currentIsPaused;
            if (_seasonSet || _isPaused) return;
            if (_time >= _timeDelay)
            {
                _seasonSet = true;
                _time = 0f;
                var seasonInverted = CheckIfInvertSeason();
                if (seasonInverted)
                {
                    SetInvertedSeason();
                }
            }
            _time += 1f * SystemAPI.Time.DeltaTime;
            if (!_gameLoaded) return;
            
            _gameLoaded = false;
            UpdateTimeAndWeather();
        }
    }
}
