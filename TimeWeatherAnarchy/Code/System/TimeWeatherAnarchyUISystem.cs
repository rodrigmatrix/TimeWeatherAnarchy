using Colossal.UI.Binding;
using Game.UI;
using System;

namespace TimeWeatherAnarchy.Code.System
{
    internal partial class TimeWeatherAnarchyUISystem : UISystemBase
    {
        private TimeAndWeatherControlSystem _timeAndWeatherControlSystem;
        
        private const string MOD_ID = "TimeWeatherAnarchyUI";
        private const string MAIN_PANEL_OPEN = "MainPanelOpen";
        private const string CURRENT_TIME = "CurrentTime";
        private const string CURRENT_TEMPERATURE = "CurrentTemperature";
        private const string TIME_OPTION = "TimeOption";
        private const string WEATHER_OPTION = "WeatherOption";
        private const string ENABLE_CUSTOM_PRECIPITATION = "EnableCustomPrecipitation";
        private const string EnableCustomClouds = "EnableCustomClouds";
        private const string EnableCustomAurora = "EnableCustomAurora";
        private const string EnableCustomTemperature = "EnableCustomTemperature";
        private const string CurrentDayOfTheYear = "CurrentDayOfTheYear";
        private const string CurrentPrecipitation = "CurrentPrecipitation";
        private const string CurrentClouds = "CurrentClouds";
        private const string CurrentAurora = "CurrentAurora";
        private const string CurrentWeatherTime = "CurrentWeatherTime";
        
        
        private ValueBinding<bool> _panelVisibleBinding;
        private ValueBinding<int> _currentOverrideTime;
        private ValueBinding<int> _currentOverrideTemperature;
        private ValueBinding<int> _timeOption;
        private ValueBinding<int> _weatherOption;
        private ValueBinding<bool> _enableCustomPrecipitation;
        private ValueBinding<bool> _enableCustomClouds;
        private ValueBinding<bool> _enableCustomAurora;
        private ValueBinding<bool> _enableCustomTemperature;
        private ValueBinding<float> _currentClouds;
        private ValueBinding<float> _currentAurora;
        private ValueBinding<float> _currentPrecipitation;
        private ValueBinding<int> _currentDayOfTheYear;
        private ValueBinding<float> _currentWeatherTime;

        protected override void OnCreate()
        {
            base.OnCreate();
            _timeAndWeatherControlSystem = World.GetOrCreateSystemManaged<TimeAndWeatherControlSystem>();
            
            // get bindings
            
            _panelVisibleBinding = new ValueBinding<bool>(MOD_ID, MAIN_PANEL_OPEN, false);
            AddBinding(_panelVisibleBinding);

            _currentOverrideTime = new ValueBinding<int>(MOD_ID, CURRENT_TIME, Mod.m_Setting.CurrentTime);
            AddBinding(_currentOverrideTime);
            
            _currentOverrideTemperature = new ValueBinding<int>(MOD_ID, CURRENT_TEMPERATURE, Mod.m_Setting.CurrentTemperature);
            AddBinding(_currentOverrideTemperature);
            
            _timeOption = new ValueBinding<int>(MOD_ID, TIME_OPTION, Mod.m_Setting.TimeOption);
            AddBinding(_timeOption);
            
            _weatherOption = new ValueBinding<int>(MOD_ID, WEATHER_OPTION, Mod.m_Setting.WeatherOption);
            AddBinding(_weatherOption);
            
            _enableCustomPrecipitation = new ValueBinding<bool>(MOD_ID, ENABLE_CUSTOM_PRECIPITATION, Mod.m_Setting.EnableCustomPrecipitation);
            AddBinding(_enableCustomPrecipitation);
            
            _enableCustomClouds = new ValueBinding<bool>(MOD_ID, EnableCustomClouds, Mod.m_Setting.EnableCustomClouds);
            AddBinding(_enableCustomClouds);
            
            _enableCustomAurora = new ValueBinding<bool>(MOD_ID, EnableCustomAurora, Mod.m_Setting.EnableCustomAurora);
            AddBinding(_enableCustomAurora);
            
            _currentPrecipitation = new ValueBinding<float>(MOD_ID, CurrentPrecipitation, Mod.m_Setting.CurrentPrecipitation);
            AddBinding(_currentPrecipitation);
            
            _currentClouds = new ValueBinding<float>(MOD_ID, CurrentClouds, Mod.m_Setting.CurrentClouds);
            AddBinding(_currentClouds);
            
            _currentAurora = new ValueBinding<float>(MOD_ID, CurrentAurora, Mod.m_Setting.CurrentAurora);
            AddBinding(_currentAurora);
            
            _currentDayOfTheYear = new ValueBinding<int>(MOD_ID, CurrentDayOfTheYear, Mod.m_Setting.CurrentDayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentDayOfTheYear = new ValueBinding<int>(MOD_ID, CurrentDayOfTheYear, Mod.m_Setting.CurrentDayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentWeatherTime = new ValueBinding<float>(MOD_ID, CurrentWeatherTime, Mod.m_Setting.CurrentWeatherTime);
            AddBinding(_currentWeatherTime);
            
            _enableCustomTemperature = new ValueBinding<bool>(MOD_ID, EnableCustomTemperature, Mod.m_Setting.EnableCustomTemperature);
            AddBinding(_enableCustomTemperature);
            
            // set bindings

            AddBinding(new TriggerBinding<bool>(MOD_ID, MAIN_PANEL_OPEN, SetPanelVisibility));

            AddBinding(new TriggerBinding<int>(MOD_ID, CURRENT_TIME, SetCurrentTime));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, CURRENT_TEMPERATURE, SetCurrentTemperature));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, TIME_OPTION, SetTimeOption));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, WEATHER_OPTION, SetWeatherOption));
            
            AddBinding(new TriggerBinding<bool>(MOD_ID, ENABLE_CUSTOM_PRECIPITATION, SetEnableCustomPrecipitation));
            
            AddBinding(new TriggerBinding<bool>(MOD_ID, EnableCustomClouds, SetEnableCustomClouds));
            
            AddBinding(new TriggerBinding<bool>(MOD_ID, EnableCustomAurora, SetEnableCustomAurora));
            
            AddBinding(new TriggerBinding<float>(MOD_ID, CurrentClouds, SetCustomClouds));
            
            AddBinding(new TriggerBinding<float>(MOD_ID, CurrentAurora, SetCustomAurora));
            
            AddBinding(new TriggerBinding<float>(MOD_ID, CurrentPrecipitation, SetCustomPrecipitation));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, CurrentDayOfTheYear, SetCustomDayOfYear));
            
            AddBinding(new TriggerBinding<bool>(MOD_ID, EnableCustomTemperature, SetEnableCustomTemperature));
            
            AddBinding(new TriggerBinding<float>(MOD_ID, CurrentWeatherTime, SetCustomWeatherTime));
        }

        private void SetPanelVisibility(bool open)
        {
            _panelVisibleBinding.Update(open);
            _timeAndWeatherControlSystem.UpdateWeather();
            _timeAndWeatherControlSystem.UpdateTime();
        }

        private void SetCurrentTime(int time)
        {
            _currentOverrideTime.Update(time);
            Mod.m_Setting.CurrentTime = time;
            _timeAndWeatherControlSystem.UpdateTime();
        }
        
        private void SetCurrentTemperature(int temperature)
        {
            _currentOverrideTemperature.Update(temperature);
            Mod.m_Setting.CurrentTemperature = temperature;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetTimeOption(int option)
        {
            _timeOption.Update(option);
            Mod.m_Setting.TimeOption = option;
            _timeAndWeatherControlSystem.UpdateTime();
        }
        
        private void SetWeatherOption(int option)
        {
            _weatherOption.Update(option);
            Mod.m_Setting.WeatherOption = option;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetEnableCustomPrecipitation(bool enabled)
        {
            _enableCustomPrecipitation.Update(enabled);
            Mod.m_Setting.EnableCustomPrecipitation = enabled;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetEnableCustomTemperature(bool enabled)
        {
            _enableCustomTemperature.Update(enabled);
            Mod.m_Setting.EnableCustomTemperature = enabled;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetEnableCustomClouds(bool enabled)
        {
            _enableCustomClouds.Update(enabled);
            Mod.m_Setting.EnableCustomClouds = enabled;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetEnableCustomAurora(bool enabled)
        {
            _enableCustomAurora.Update(enabled);
            Mod.m_Setting.EnableCustomAurora = enabled;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetCustomDayOfYear(int dayOfYear)
        {
            _currentDayOfTheYear.Update(dayOfYear);
            Mod.m_Setting.CurrentDayOfTheYear = dayOfYear;
            _timeAndWeatherControlSystem.UpdateTime();
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetCustomClouds(float clouds)
        {
            _currentClouds.Update(clouds);
            Mod.m_Setting.CurrentClouds = clouds;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetCustomPrecipitation(float precipitation)
        {
            _currentPrecipitation.Update(precipitation);
            Mod.m_Setting.CurrentPrecipitation = precipitation;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetCustomAurora(float aurora)
        {
            _currentAurora.Update(aurora);
            Mod.m_Setting.CurrentAurora = aurora;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
        
        private void SetCustomWeatherTime(float time)
        {
            _currentWeatherTime.Update(time);
            Mod.m_Setting.CurrentWeatherTime = time;
            _timeAndWeatherControlSystem.UpdateWeather();
        }
    }
}
