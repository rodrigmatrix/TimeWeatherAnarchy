using System.Collections.Generic;
using System.Linq;
using Colossal.UI.Binding;
using Game;
using Game.Input;
using Game.SceneFlow;
using Game.Settings;
using TimeWeatherAnarchy.Code.Domain;
using TimeWeatherAnarchy.Code.Settings;
using TimeWeatherAnarchy.Code.Utils;

namespace TimeWeatherAnarchy.Code.System
{
    internal partial class TimeWeatherAnarchyUISystem : ExtendedUISystemBase
    {
        private TimeAndWeatherControlSystem _timeAndWeatherControlSystem;
        
        private const string ModID = "TimeWeatherAnarchy";
        private const string Profiles = "Profiles";
        private const string SelectedProfile = "SelectedProfile";
        private const string UpdateProfile = "UpdateProfile";
        private const string CreateProfile = "CreateProfile";
        private const string DeleteProfile = "DeleteProfile";
        private const string MainPanelOpen = "MainPanelOpen";
        private const string CurrentTime = "CurrentTime";
        private const string Temperature = "CurrentTemperature";
        private const string TimeOption = "TimeOption";
        private const string WeatherOption = "WeatherOption";
        private const string EnableCustomPrecipitation = "EnableCustomPrecipitation";
        private const string EnableCustomClouds = "EnableCustomClouds";
        private const string EnableCustomAurora = "EnableCustomAurora";
        private const string EnableCustomTemperature = "EnableCustomTemperature";
        private const string EnableCustomFog = "EnableCustomFog";
        private const string EnableCustomThunder = "EnableCustomThunder";
        private const string DayOfTheYear = "CurrentDayOfTheYear";
        private const string Precipitation = "CurrentPrecipitation";
        private const string Clouds = "CurrentClouds";
        private const string Aurora = "CurrentAurora";
        private const string Fog = "CurrentFog";
        private const string Thunder = "CurrentThunder";
        private const string Rainbow = "CurrentRainbow";
        private const string WeatherTime = "CurrentWeatherTime";
        private const string CustomLatitude = "CustomLatitude";
        private const string CustomLongitude = "CustomLongitude";
        private const string TimePreference = "TimePreference";
        private const string TemperaturePreference = "TemperaturePreference";
        
        private ValueBindingHelper<string> _selectedProfile;
        private ValueBindingHelper<List<TimeWeatherProfileUI>> _profiles;
        private ValueBinding<bool> _panelVisibleBinding;
        private ValueBinding<float> _currentOverrideTime;
        private ValueBinding<int> _currentOverrideTemperature;
        private ValueBinding<int> _timeOption;
        private ValueBinding<int> _weatherOption;
        private ValueBinding<bool> _enableCustomPrecipitation;
        private ValueBinding<bool> _enableCustomClouds;
        private ValueBinding<bool> _enableCustomAurora;
        private ValueBinding<bool> _enableCustomTemperature;
        private ValueBinding<bool> _enableCustomFog;
        private ValueBinding<bool> _enableCustomThunder;
        private ValueBinding<float> _currentClouds;
        private ValueBinding<float> _currentAurora;
        private ValueBinding<float> _currentPrecipitation;
        private ValueBinding<float> _currentFog;
        private ValueBinding<float> _currentThunder;
        private ValueBinding<float> _currentRainbow;
        private ValueBinding<int> _currentDayOfTheYear;
        private ValueBinding<float> _currentWeatherTime;
        private ValueBindingHelper<float> _currentLatitude;
        private ValueBindingHelper<float> _currentLongitude;
        private ValueBinding<int> _timePreference;
        private ValueBinding<int> _temperaturePreference;
        private ProxyAction _toggleMainPanelBinding;
        private ProxyAction _toggleDayNightTimeBinding;
        private ProxyAction _toggleNextProfileBinding;

        protected override void OnCreate()
        {
            base.OnCreate();
            _timeAndWeatherControlSystem = World.GetOrCreateSystemManaged<TimeAndWeatherControlSystem>();
            
            _toggleMainPanelBinding = Mod.m_Setting.GetAction(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle));
            _toggleMainPanelBinding.shouldBeEnabled = true;
            
            _toggleDayNightTimeBinding = Mod.m_Setting.GetAction(nameof(TimeWeatherAnarchySettings.TriggerDayNightToggle));
            _toggleDayNightTimeBinding.shouldBeEnabled = true;
            
            _toggleNextProfileBinding = Mod.m_Setting.GetAction(nameof(TimeWeatherAnarchySettings.TriggerNextProfileToggle));
            _toggleNextProfileBinding.shouldBeEnabled = true;
            
            // set bindings
            _panelVisibleBinding = new ValueBinding<bool>(ModID, MainPanelOpen, false);
            AddBinding(_panelVisibleBinding);

            _currentOverrideTime = new ValueBinding<float>(ModID, CurrentTime, Mod.m_Setting.Profile.Time);
            AddBinding(_currentOverrideTime);
            
            _currentOverrideTemperature = new ValueBinding<int>(ModID, Temperature, Mod.m_Setting.Profile.Temperature);
            AddBinding(_currentOverrideTemperature);
            
            _timeOption = new ValueBinding<int>(ModID, TimeOption, Mod.m_Setting.Profile.TimeOption);
            AddBinding(_timeOption);
            
            _weatherOption = new ValueBinding<int>(ModID, WeatherOption, Mod.m_Setting.Profile.WeatherOption);
            AddBinding(_weatherOption);
            
            _enableCustomPrecipitation = new ValueBinding<bool>(ModID, EnableCustomPrecipitation, Mod.m_Setting.Profile.EnableCustomPrecipitation);
            AddBinding(_enableCustomPrecipitation);
            
            _enableCustomClouds = new ValueBinding<bool>(ModID, EnableCustomClouds, Mod.m_Setting.Profile.EnableCustomClouds);
            AddBinding(_enableCustomClouds);
            
            _enableCustomAurora = new ValueBinding<bool>(ModID, EnableCustomAurora, Mod.m_Setting.Profile.EnableCustomAurora);
            AddBinding(_enableCustomAurora);
            
            _currentPrecipitation = new ValueBinding<float>(ModID, Precipitation, Mod.m_Setting.Profile.Precipitation);
            AddBinding(_currentPrecipitation);
            
            _currentClouds = new ValueBinding<float>(ModID, Clouds, Mod.m_Setting.Profile.Clouds);
            AddBinding(_currentClouds);
            
            _currentAurora = new ValueBinding<float>(ModID, Aurora, Mod.m_Setting.Profile.Aurora);
            AddBinding(_currentAurora);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, DayOfTheYear, Mod.m_Setting.Profile.DayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, DayOfTheYear, Mod.m_Setting.Profile.DayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentWeatherTime = new ValueBinding<float>(ModID, WeatherTime, Mod.m_Setting.Profile.WeatherTime);
            AddBinding(_currentWeatherTime);
            
            _enableCustomTemperature = new ValueBinding<bool>(ModID, EnableCustomTemperature, Mod.m_Setting.Profile.EnableCustomTemperature);
            AddBinding(_enableCustomTemperature);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, DayOfTheYear, Mod.m_Setting.Profile.DayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentWeatherTime = new ValueBinding<float>(ModID, WeatherTime, Mod.m_Setting.Profile.WeatherTime);
            AddBinding(_currentWeatherTime);
            
            _currentFog = new ValueBinding<float>(ModID, Fog, Mod.m_Setting.Profile.Fog);
            AddBinding(_currentFog);
            
            _currentThunder = new ValueBinding<float>(ModID, Thunder, Mod.m_Setting.Profile.Thunder);
            AddBinding(_currentThunder);
            
            _currentRainbow = new ValueBinding<float>(ModID, Rainbow, Mod.m_Setting.Profile.Rainbow);
            AddBinding(_currentRainbow);
            
            _enableCustomTemperature = new ValueBinding<bool>(ModID, EnableCustomTemperature, Mod.m_Setting.Profile.EnableCustomTemperature);
            AddBinding(_enableCustomTemperature);
            
            _enableCustomFog = new ValueBinding<bool>(ModID, EnableCustomFog, Mod.m_Setting.Profile.EnableCustomFog);
            AddBinding(_enableCustomFog);
            
            _enableCustomThunder = new ValueBinding<bool>(ModID, EnableCustomThunder, Mod.m_Setting.Profile.EnableCustomThunder);
            AddBinding(_enableCustomThunder);
            
            _timePreference = new ValueBinding<int>(ModID, TimePreference, GetTimePreference());
            AddBinding(_timePreference);
            
            _temperaturePreference = new ValueBinding<int>(ModID, TemperaturePreference, GetTemperaturePreference());
            AddBinding(_temperaturePreference);
            
            _selectedProfile = CreateBinding(SelectedProfile, Mod.m_Setting.SelectedProfile);
            
            _profiles = CreateBinding(Profiles, Mod.m_Setting.Profiles.ToUI());
            
            _currentLatitude = CreateBinding(CustomLatitude, Mod.m_Setting.Profile.Latitude);
            
            _currentLongitude = CreateBinding(CustomLongitude, Mod.m_Setting.Profile.Longitude);
            
            CreateTrigger<string>(SelectedProfile, SetProfile);
            
            CreateTrigger<float>(CustomLatitude, SetCustomLatitude);
            
            CreateTrigger<float>(CustomLongitude, SetCustomLongitude);
            
            // set triggers
            AddBinding(new TriggerBinding<bool>(ModID, MainPanelOpen, SetPanelVisibility));

            AddBinding(new TriggerBinding<float>(ModID, CurrentTime, SetCurrentTime));
            
            AddBinding(new TriggerBinding<int>(ModID, Temperature, SetCurrentTemperature));
            
            AddBinding(new TriggerBinding<int>(ModID, TimeOption, SetTimeOption));
            
            AddBinding(new TriggerBinding<int>(ModID, WeatherOption, SetWeatherOption));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomPrecipitation, SetEnableCustomPrecipitation));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomTemperature, SetEnableCustomTemperature));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomClouds, SetEnableCustomClouds));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomAurora, SetEnableCustomAurora));

            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomFog, SetEnableCustomFog));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomThunder, SetEnableCustomThunder));
            
            AddBinding(new TriggerBinding<float>(ModID, Clouds, SetCustomClouds));
            
            AddBinding(new TriggerBinding<float>(ModID, Aurora, SetCustomAurora));
            
            AddBinding(new TriggerBinding<float>(ModID, Precipitation, SetCustomPrecipitation));
            
            AddBinding(new TriggerBinding<float>(ModID, Aurora, SetCustomAurora));
            
            AddBinding(new TriggerBinding<float>(ModID, Precipitation, SetCustomPrecipitation));
            
            AddBinding(new TriggerBinding<int>(ModID, DayOfTheYear, SetCustomDayOfYear));
            
            AddBinding(new TriggerBinding<float>(ModID, WeatherTime, SetCustomWeatherTime));
            
            AddBinding(new TriggerBinding<float>(ModID, Fog, SetCustomFog));
            
            AddBinding(new TriggerBinding<float>(ModID, Thunder, SetCustomThunder));
            
            AddBinding(new TriggerBinding<float>(ModID, Rainbow, SetCustomRainbow));
            
            //AddBinding(new TriggerBinding<List<TimeWeatherProfile>>(ModID, Profiles, SetProfilesList));
            
            AddBinding(new TriggerBinding<string, bool>(ModID, CreateProfile, OnCreateProfile));
            
            AddBinding(new TriggerBinding<string, string>(ModID, UpdateProfile, OnUpdateProfile));
            
            AddBinding(new TriggerBinding<string>(ModID, DeleteProfile, OnDeleteProfile));

            GameManager.instance.settings.userInterface.onSettingsApplied += _ =>
            {
                _temperaturePreference.Update(GetTemperaturePreference());
                _timePreference.Update(GetTimePreference());
            };
        }
        
        protected override void OnUpdate()
        {
            if (_toggleMainPanelBinding.WasPerformedThisFrame())
            {
                OnMainPanelToolTrigger();
            }
            
            if (_toggleDayNightTimeBinding.WasPerformedThisFrame())
            {
                OnDayNightToolTrigger();
            }
            
            if (_toggleNextProfileBinding.WasPerformedThisFrame())
            {
                OnNextProfileClickedTrigger();
            }

            base.OnUpdate();
        }

        private int GetTimePreference()
        {
            var timePreference = Domain.TimePreference.TwentyFourHours;
            timePreference = GameManager.instance.settings.userInterface.timeFormat switch
            {
                InterfaceSettings.TimeFormat.TwentyFourHours => Domain.TimePreference.TwentyFourHours,
                InterfaceSettings.TimeFormat.TwelveHours => Domain.TimePreference.TwelveHours,
                _ => timePreference
            };
            return (int) timePreference;
        }
        
        private int GetTemperaturePreference()
        {
            var temperaturePreference = Domain.TemperaturePreference.Celsius;
            temperaturePreference = GameManager.instance.settings.userInterface.temperatureUnit switch
            {
                InterfaceSettings.TemperatureUnit.Celsius => Domain.TemperaturePreference.Celsius,
                InterfaceSettings.TemperatureUnit.Fahrenheit => Domain.TemperaturePreference.Fahrenheit,
                InterfaceSettings.TemperatureUnit.Kelvin => Domain.TemperaturePreference.Kelvin,
                _ => temperaturePreference
            };

            return (int) temperaturePreference;
        }

        private void SetPanelVisibility(bool open)
        {
            _panelVisibleBinding.Update(open);
            Mod.m_Setting.InitializeProfiles();
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void OnMainPanelToolTrigger()
        {
            SetPanelVisibility(!_panelVisibleBinding.value);
        }
        
        private void OnDayNightToolTrigger()
        {
            if (_timeOption.value == (int) TimeOptions.Day)
            {
                SetTimeOption((int) TimeOptions.Night);
            }
            else
            {
                SetTimeOption((int) TimeOptions.Day);
            }
        }
        
        private void OnNextProfileClickedTrigger()
        {
            Mod.m_Setting.SelectedProfile = Mod.m_Setting.Profiles.Next(Mod.m_Setting.Profile).Id;
            UpdateUIFields();
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }

        private void SetCurrentTime(float time)
        {
            _currentOverrideTime.Update(time);
            Mod.m_Setting.Profile.Time = time;
            _timeAndWeatherControlSystem.UpdateTime();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCurrentTemperature(int temperature)
        {
            _currentOverrideTemperature.Update(temperature);
            Mod.m_Setting.Profile.Temperature = temperature;
            _timeAndWeatherControlSystem.UpdateTemperature();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetTimeOption(int option)
        {
            _timeOption.Update(option);
            Mod.m_Setting.Profile.TimeOption = option;
            _timeAndWeatherControlSystem.UpdateTime();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetWeatherOption(int option)
        {
            _weatherOption.Update(option);
            Mod.m_Setting.Profile.WeatherOption = option;
            _timeAndWeatherControlSystem.UpdateSeason();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetEnableCustomPrecipitation(bool enabled)
        {
            _enableCustomPrecipitation.Update(enabled);
            Mod.m_Setting.Profile.EnableCustomPrecipitation = enabled;
            _timeAndWeatherControlSystem.UpdatePrecipitation();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetEnableCustomTemperature(bool enabled)
        {
            _enableCustomTemperature.Update(enabled);
            Mod.m_Setting.Profile.EnableCustomTemperature = enabled;
            _timeAndWeatherControlSystem.UpdateTemperature();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetEnableCustomFog(bool enabled)
        {
            _enableCustomFog.Update(enabled);
            Mod.m_Setting.Profile.EnableCustomFog = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetEnableCustomThunder(bool enabled)
        {
            _enableCustomThunder.Update(enabled);
            Mod.m_Setting.Profile.EnableCustomThunder = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetEnableCustomClouds(bool enabled)
        {
            _enableCustomClouds.Update(enabled);
            Mod.m_Setting.Profile.EnableCustomClouds = enabled;
            _timeAndWeatherControlSystem.UpdateClouds();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetEnableCustomAurora(bool enabled)
        {
            _enableCustomAurora.Update(enabled);
            Mod.m_Setting.Profile.EnableCustomAurora = enabled;
            _timeAndWeatherControlSystem.UpdateAurora();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomDayOfYear(int dayOfYear)
        {
            _currentDayOfTheYear.Update(dayOfYear);
            Mod.m_Setting.Profile.DayOfTheYear = dayOfYear;
            _timeAndWeatherControlSystem.UpdateTime();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomClouds(float clouds)
        {
            _currentClouds.Update(clouds);
            Mod.m_Setting.Profile.Clouds = clouds;
            _timeAndWeatherControlSystem.UpdateClouds();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomPrecipitation(float precipitation)
        {
            _currentPrecipitation.Update(precipitation);
            Mod.m_Setting.Profile.Precipitation = precipitation;
            _timeAndWeatherControlSystem.UpdatePrecipitation();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomAurora(float aurora)
        {
            _currentAurora.Update(aurora);
            Mod.m_Setting.Profile.Aurora = aurora;
            _timeAndWeatherControlSystem.UpdateAurora();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomWeatherTime(float time)
        {
            _currentWeatherTime.Update(time);
            Mod.m_Setting.Profile.WeatherTime = time;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomFog(float fog)
        {
            _currentFog.Update(fog);
            Mod.m_Setting.Profile.Fog = fog;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomLatitude(float latitude)
        {
            _currentLatitude.Value = latitude;
            Mod.m_Setting.Profile.Latitude = latitude;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomLongitude(float longitude)
        {
            _currentLongitude.Value = longitude;
            Mod.m_Setting.Profile.Longitude = longitude;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomThunder(float thunder)
        {
            _currentThunder.Update(thunder);
            Mod.m_Setting.Profile.Thunder = thunder;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetCustomRainbow(float rainbow)
        {
            _currentRainbow.Update(rainbow);
            Mod.m_Setting.Profile.Rainbow = rainbow;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
            ProfileUtils.Save(Mod.m_Setting.Profile);
        }
        
        private void SetProfile(string id)
        {
            Mod.m_Setting.SelectedProfile = id;
            UpdateUIFields();
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void OnCreateProfile(string name, bool copyCurrentProfile)
        {
            Mod.m_Setting.CreateProfile(name, copyCurrentProfile);
            UpdateUIFields();
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void OnDeleteProfile(string id)
        {
            Mod.m_Setting.DeleteProfile(id);
            UpdateUIFields();
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }

        private void OnUpdateProfile(string id, string name)
        {
            Mod.m_Setting.UpdateProfile(id, name);  
            UpdateUIFields();
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }

        private void UpdateUIFields()
        {
            var profile = Mod.m_Setting.Profile;
            _currentOverrideTime.Update(profile.Time);
            _currentOverrideTemperature.Update(profile.Temperature);
            _currentClouds.Update(profile.Clouds);
            _currentPrecipitation.Update(profile.Precipitation);
            _currentAurora.Update(profile.Aurora);
            _timeOption.Update(profile.TimeOption);
            _weatherOption.Update(profile.WeatherOption);
            _enableCustomThunder.Update(profile.EnableCustomThunder);
            _enableCustomClouds.Update(profile.EnableCustomClouds);
            _enableCustomTemperature.Update(profile.EnableCustomTemperature);
            _enableCustomPrecipitation.Update(profile.EnableCustomPrecipitation);
            _enableCustomAurora.Update(profile.EnableCustomAurora);
            _currentDayOfTheYear.Update(profile.DayOfTheYear);
            _profiles.Value = Mod.m_Setting.Profiles.ToUI();
            _selectedProfile.Value = Mod.m_Setting.SelectedProfile;
        }
    }
}
