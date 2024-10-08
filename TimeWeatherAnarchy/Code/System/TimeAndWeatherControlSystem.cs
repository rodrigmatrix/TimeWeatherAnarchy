﻿using System;
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
        private bool _gameLoaded;

        protected override void OnCreate()
        {
            base.OnCreate();
            _climateSystem = World.GetOrCreateSystemManaged<ClimateSystem>();
            _planetarySystem = World.GetOrCreateSystemManaged<PlanetarySystem>();
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
                Mod.log.Warn(e + " error occured while updating time and weather.");
                throw;
            }
        }

        private void UpdateWeather()
        {
            if (_isEditor) return;

            //_climateSystem.fog.overrideValue = Mod.m_Setting.Fog;
            //_climateSystem.thunder.overrideValue = Mod.m_Setting.Thunder;
            _climateSystem.temperature.overrideValue = Mod.m_Setting.Temperature;
            _climateSystem.precipitation.overrideValue = Mod.m_Setting.Precipitation;
            _climateSystem.cloudiness.overrideValue = Mod.m_Setting.Clouds;
            _climateSystem.aurora.overrideValue = Mod.m_Setting.Aurora;
            //_climateSystem.rainbow = Mod.m_Setting.Rainbow;
            _climateSystem.temperature.overrideState = Mod.m_Setting.EnableCustomTemperature;
            _climateSystem.precipitation.overrideState = Mod.m_Setting.EnableCustomPrecipitation;
            _climateSystem.cloudiness.overrideState = Mod.m_Setting.EnableCustomClouds;
            _climateSystem.aurora.overrideState = Mod.m_Setting.EnableCustomAurora;
            //_climateSystem.fog.overrideState = Mod.m_Setting.EnableCustomFog;
            //_climateSystem.thunder.overrideState = Mod.m_Setting.EnableCustomThunder;
            _climateSystem.currentDate.overrideState = Mod.m_Setting.WeatherOption != (int) WeatherOptions.Default;
            
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
                    _climateSystem.currentDate.overrideValue = Mod.m_Setting.WeatherTime;
                    break;
            };
           
        }

        private void UpdateTime()
        {
            if (_isEditor) return;
            _planetarySystem.overrideTime = Mod.m_Setting.TimeOption != (int) TimeOptions.Default;
            _planetarySystem.dayOfYear = Mod.m_Setting.DayOfTheYear;
            _currentTime = Mod.m_Setting.TimeOption switch
            {
                ((int) TimeOptions.Default) => 0,
                ((int) TimeOptions.Day) => 12,
                ((int) TimeOptions.Night) => 1,
                ((int) TimeOptions.Custom) => Mod.m_Setting.Time,
                _ => Mod.m_Setting.Time,
            };
            _planetarySystem.time = _currentTime;
        }

        protected override void OnUpdate()
        {
           
            if (!_gameLoaded) return;
            
            _gameLoaded = false;
            UpdateTimeAndWeather();
        }
    }
}
