using Colossal.UI.Binding;
using Game.Input;
using Game.UI;
using TimeWeatherAnarchy.Code.Settings;

namespace TimeWeatherAnarchy.Code.System
{
    internal partial class TimeWeatherAnarchyUISystem : UISystemBase
    {
        private TimeAndWeatherControlSystem _timeAndWeatherControlSystem;
        
        private const string ModID = "TimeWeatherAnarchy";
        private const string MainPanelOpen = "MainPanelOpen";
        private const string CurrentTime = "CurrentTime";
        private const string CurrentTemperature = "CurrentTemperature";
        private const string TimeOption = "TimeOption";
        private const string WeatherOption = "WeatherOption";
        private const string EnableCustomPrecipitation = "EnableCustomPrecipitation";
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
        private ProxyAction _toggleMainPanelBinding;

        protected override void OnCreate()
        {
            base.OnCreate();
            _timeAndWeatherControlSystem = World.GetOrCreateSystemManaged<TimeAndWeatherControlSystem>();
            
            _toggleMainPanelBinding = Mod.m_Setting.GetAction(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle));
            _toggleMainPanelBinding.shouldBeEnabled = true;
            
            // set bindings
            _panelVisibleBinding = new ValueBinding<bool>(ModID, MainPanelOpen, false);
            AddBinding(_panelVisibleBinding);

            _currentOverrideTime = new ValueBinding<int>(ModID, CurrentTime, Mod.m_Setting.CurrentTime);
            AddBinding(_currentOverrideTime);
            
            _currentOverrideTemperature = new ValueBinding<int>(ModID, CurrentTemperature, Mod.m_Setting.CurrentTemperature);
            AddBinding(_currentOverrideTemperature);
            
            _timeOption = new ValueBinding<int>(ModID, TimeOption, Mod.m_Setting.TimeOption);
            AddBinding(_timeOption);
            
            _weatherOption = new ValueBinding<int>(ModID, WeatherOption, Mod.m_Setting.WeatherOption);
            AddBinding(_weatherOption);
            
            _enableCustomPrecipitation = new ValueBinding<bool>(ModID, EnableCustomPrecipitation, Mod.m_Setting.EnableCustomPrecipitation);
            AddBinding(_enableCustomPrecipitation);
            
            _enableCustomClouds = new ValueBinding<bool>(ModID, EnableCustomClouds, Mod.m_Setting.EnableCustomClouds);
            AddBinding(_enableCustomClouds);
            
            _enableCustomAurora = new ValueBinding<bool>(ModID, EnableCustomAurora, Mod.m_Setting.EnableCustomAurora);
            AddBinding(_enableCustomAurora);
            
            _currentPrecipitation = new ValueBinding<float>(ModID, CurrentPrecipitation, Mod.m_Setting.CurrentPrecipitation);
            AddBinding(_currentPrecipitation);
            
            _currentClouds = new ValueBinding<float>(ModID, CurrentClouds, Mod.m_Setting.CurrentClouds);
            AddBinding(_currentClouds);
            
            _currentAurora = new ValueBinding<float>(ModID, CurrentAurora, Mod.m_Setting.CurrentAurora);
            AddBinding(_currentAurora);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, CurrentDayOfTheYear, Mod.m_Setting.CurrentDayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, CurrentDayOfTheYear, Mod.m_Setting.CurrentDayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentWeatherTime = new ValueBinding<float>(ModID, CurrentWeatherTime, Mod.m_Setting.CurrentWeatherTime);
            AddBinding(_currentWeatherTime);
            
            _enableCustomTemperature = new ValueBinding<bool>(ModID, EnableCustomTemperature, Mod.m_Setting.EnableCustomTemperature);
            AddBinding(_enableCustomTemperature);
            
            // set triggers
            AddBinding(new TriggerBinding<bool>(ModID, MainPanelOpen, SetPanelVisibility));

            AddBinding(new TriggerBinding<int>(ModID, CurrentTime, SetCurrentTime));
            
            AddBinding(new TriggerBinding<int>(ModID, CurrentTemperature, SetCurrentTemperature));
            
            AddBinding(new TriggerBinding<int>(ModID, TimeOption, SetTimeOption));
            
            AddBinding(new TriggerBinding<int>(ModID, WeatherOption, SetWeatherOption));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomPrecipitation, SetEnableCustomPrecipitation));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomClouds, SetEnableCustomClouds));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomAurora, SetEnableCustomAurora));
            
            AddBinding(new TriggerBinding<float>(ModID, CurrentClouds, SetCustomClouds));
            
            AddBinding(new TriggerBinding<float>(ModID, CurrentAurora, SetCustomAurora));
            
            AddBinding(new TriggerBinding<float>(ModID, CurrentPrecipitation, SetCustomPrecipitation));
            
            AddBinding(new TriggerBinding<int>(ModID, CurrentDayOfTheYear, SetCustomDayOfYear));
            
            AddBinding(new TriggerBinding<bool>(ModID, EnableCustomTemperature, SetEnableCustomTemperature));
            
            AddBinding(new TriggerBinding<float>(ModID, CurrentWeatherTime, SetCustomWeatherTime));
        }
        
        protected override void OnUpdate()
        {
            if (_toggleMainPanelBinding.WasPerformedThisFrame())
            {
                OnMainPanelToolTrigger();
            }

            base.OnUpdate();
        }

        private void SetPanelVisibility(bool open)
        {
            _panelVisibleBinding.Update(open);
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void OnMainPanelToolTrigger()
        {
            SetPanelVisibility(!_panelVisibleBinding.value);
        }

        private void SetCurrentTime(int time)
        {
            _currentOverrideTime.Update(time);
            Mod.m_Setting.CurrentTime = time;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCurrentTemperature(int temperature)
        {
            _currentOverrideTemperature.Update(temperature);
            Mod.m_Setting.CurrentTemperature = temperature;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetTimeOption(int option)
        {
            _timeOption.Update(option);
            Mod.m_Setting.TimeOption = option;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetWeatherOption(int option)
        {
            _weatherOption.Update(option);
            Mod.m_Setting.WeatherOption = option;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetEnableCustomPrecipitation(bool enabled)
        {
            _enableCustomPrecipitation.Update(enabled);
            Mod.m_Setting.EnableCustomPrecipitation = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetEnableCustomTemperature(bool enabled)
        {
            _enableCustomTemperature.Update(enabled);
            Mod.m_Setting.EnableCustomTemperature = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetEnableCustomClouds(bool enabled)
        {
            _enableCustomClouds.Update(enabled);
            Mod.m_Setting.EnableCustomClouds = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetEnableCustomAurora(bool enabled)
        {
            _enableCustomAurora.Update(enabled);
            Mod.m_Setting.EnableCustomAurora = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomDayOfYear(int dayOfYear)
        {
            _currentDayOfTheYear.Update(dayOfYear);
            Mod.m_Setting.CurrentDayOfTheYear = dayOfYear;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomClouds(float clouds)
        {
            _currentClouds.Update(clouds);
            Mod.m_Setting.CurrentClouds = clouds;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomPrecipitation(float precipitation)
        {
            _currentPrecipitation.Update(precipitation);
            Mod.m_Setting.CurrentPrecipitation = precipitation;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomAurora(float aurora)
        {
            _currentAurora.Update(aurora);
            Mod.m_Setting.CurrentAurora = aurora;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomWeatherTime(float time)
        {
            _currentWeatherTime.Update(time);
            Mod.m_Setting.CurrentWeatherTime = time;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
    }
}
