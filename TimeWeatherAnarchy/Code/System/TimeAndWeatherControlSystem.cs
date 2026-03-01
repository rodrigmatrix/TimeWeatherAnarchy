using System;
using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Serialization.Entities;
using Game;
using Game.Assets;
using Game.SceneFlow;
using Game.Serialization;
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
        private string _currentSaveName = string.Empty;
        private string _currentSaveGuid = string.Empty;

        public string CurrentSaveName => _currentSaveName;
        public string CurrentSaveGuid => _currentSaveGuid;

        private const string SPRING_SEASON = "SeasonSpring";
        private const string SUMMER_SEASON = "SeasonSummer";
        private const string FALL_SEASON = "SeasonAutumn";
        private const string WINTER_SEASON = "SeasonWinter";

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
            if (_isEditor) return;

            _gameLoaded = true;

            UpdateSaveInfo();
            UpdateTimeAndWeather();
        }

        public void UpdateSaveInfo()
        {
            try
            {
                var loadSystem = World.GetOrCreateSystemManaged<LoadGameSystem>();
                var activeGuid = loadSystem.context.instigatorGuid;
                if (!AssetDatabase.global.TryGetAsset(activeGuid, out SaveGameMetadata saveMetadata))
                    return;

                _currentSaveGuid = saveMetadata.target.sessionGuid.ToString();
                _currentSaveName = $"{saveMetadata.target.cityName} - Population: {saveMetadata.target.population:N0}";
                Mod.m_Setting.UpdateSaveDisplayName(_currentSaveGuid, _currentSaveName);
                ApplyLinkedProfile(_currentSaveGuid);
            }
            catch (Exception e)
            {
                Mod.log.Info(e + " error occurred while reading save info.");
            }
        }

        private void ApplyLinkedProfile(string saveName)
        {
            if (string.IsNullOrEmpty(saveName)) return;
            var linkedProfileId = Mod.m_Setting.GetLinkedProfile(saveName);
            if (string.IsNullOrEmpty(linkedProfileId)) return;
            var linked = Mod.m_Setting.Profiles.Find(p => p.Id == linkedProfileId);
            if (linked != null)
                Mod.m_Setting.SelectedProfile = linked.Id;
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
            UpdateFog();
            UpdateSeason();
            //_climateSystem.thunder.overrideValue = Mod.m_Setting.Thunder;
            //_climateSystem.rainbow = Mod.m_Setting.Rainbow;
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
        
        private bool IsProfileActive()
        {
            var profile = Mod.m_Setting.Profile;

            if (profile.TimeOption != (int)TimeOptions.Default)
                return true;

            var activeTime = (ProfileActiveTime)profile.ProfileActiveTime;
            if (activeTime == ProfileActiveTime.Always)
                return true;

            var hour = _planetarySystem.time;
            var dayStart = Mod.m_Setting.DayStartHour;
            var dayEnd = Mod.m_Setting.DayEndHour;
            var isDaytime = hour >= dayStart && hour < dayEnd;

            return activeTime == ProfileActiveTime.Day ? isDaytime : !isDaytime;
        }

        public void UpdateAurora()
        {
            var active = IsProfileActive();
            _climateSystem.aurora.overrideValue = active ? Mod.m_Setting.Profile.Aurora : 0f;
            _climateSystem.aurora.overrideState = active && Mod.m_Setting.Profile.EnableCustomAurora;
        }
        
        public void UpdateTemperature()
        {
            var active = IsProfileActive();
            _climateSystem.temperature.overrideValue = active ? Mod.m_Setting.Profile.Temperature : 0;
            _climateSystem.temperature.overrideState = active && Mod.m_Setting.Profile.EnableCustomTemperature;
        }
        
        public void UpdateClouds()
        {
            var active = IsProfileActive();
            _climateSystem.cloudiness.overrideValue = active ? Mod.m_Setting.Profile.Clouds : 0f;
            _climateSystem.cloudiness.overrideState = active && Mod.m_Setting.Profile.EnableCustomClouds;
        }


        public void UpdatePrecipitation()
        {
            var active = IsProfileActive();
            _climateSystem.precipitation.overrideValue = active ? Mod.m_Setting.Profile.Precipitation : 0f;
            _climateSystem.precipitation.overrideState = active && Mod.m_Setting.Profile.EnableCustomPrecipitation;
        }

        public void UpdateFog()
        {
            var active = IsProfileActive();
            _climateSystem.fog.overrideValue = active ? Mod.m_Setting.Profile.Fog : 0f;
            _climateSystem.fog.overrideState = active && Mod.m_Setting.Profile.EnableCustomFog;
        }

        public void UpdateSeason()
        {
            if (IsProfileActive())
            {
                SetSeason();
                _climateSystem.currentDate.overrideState = Mod.m_Setting.Profile.WeatherOption != (int) WeatherOptions.Default;
            }
            else
            {
                _climateSystem.currentDate.overrideState = false;
            }
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
                    if (_climateSystem.currentSeasonName != SPRING_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Summer):
                    if (_climateSystem.currentSeasonName != SUMMER_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Fall):
                    if (_climateSystem.currentSeasonName != FALL_SEASON)
                    {
                        invertSeason = true;
                    }
                    break;
                case ((int)WeatherOptions.Winter): 
                    if (_climateSystem.currentSeasonName != WINTER_SEASON)
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
            // Time override is only gated by TimeOption, not by ProfileActiveTime
            // (because IsProfileActive itself uses planetary time when TimeOption == Default).
            _planetarySystem.overrideTime = Mod.m_Setting.Profile.TimeOption != (int) TimeOptions.Default;
            _planetarySystem.dayOfYear = Mod.m_Setting.Profile.DayOfTheYear;
            UpdateLatitude();
            UpdateLongitude();
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
