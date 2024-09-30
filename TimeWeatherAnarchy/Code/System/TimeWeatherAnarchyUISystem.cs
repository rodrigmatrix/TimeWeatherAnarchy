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
        private ProxyAction _toggleMainPanelBinding;
        private ProxyAction _toggleDayNightTimeBinding;

        protected override void OnCreate()
        {
            base.OnCreate();
            _timeAndWeatherControlSystem = World.GetOrCreateSystemManaged<TimeAndWeatherControlSystem>();
            
            _toggleMainPanelBinding = Mod.m_Setting.GetAction(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle));
            _toggleMainPanelBinding.shouldBeEnabled = true;
            
            _toggleDayNightTimeBinding = Mod.m_Setting.GetAction(nameof(TimeWeatherAnarchySettings.TriggerDayNightToggle));
            _toggleDayNightTimeBinding.shouldBeEnabled = true;
            
            // set bindings
            _panelVisibleBinding = new ValueBinding<bool>(ModID, MainPanelOpen, false);
            AddBinding(_panelVisibleBinding);

            _currentOverrideTime = new ValueBinding<float>(ModID, CurrentTime, Mod.m_Setting.Time);
            AddBinding(_currentOverrideTime);
            
            _currentOverrideTemperature = new ValueBinding<int>(ModID, Temperature, Mod.m_Setting.Temperature);
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
            
            _currentPrecipitation = new ValueBinding<float>(ModID, Precipitation, Mod.m_Setting.Precipitation);
            AddBinding(_currentPrecipitation);
            
            _currentClouds = new ValueBinding<float>(ModID, Clouds, Mod.m_Setting.Clouds);
            AddBinding(_currentClouds);
            
            _currentAurora = new ValueBinding<float>(ModID, Aurora, Mod.m_Setting.Aurora);
            AddBinding(_currentAurora);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, DayOfTheYear, Mod.m_Setting.DayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, DayOfTheYear, Mod.m_Setting.DayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentWeatherTime = new ValueBinding<float>(ModID, WeatherTime, Mod.m_Setting.WeatherTime);
            AddBinding(_currentWeatherTime);
            
            _enableCustomTemperature = new ValueBinding<bool>(ModID, EnableCustomTemperature, Mod.m_Setting.EnableCustomTemperature);
            AddBinding(_enableCustomTemperature);
            
            _currentDayOfTheYear = new ValueBinding<int>(ModID, DayOfTheYear, Mod.m_Setting.DayOfTheYear);
            AddBinding(_currentDayOfTheYear);
            
            _currentWeatherTime = new ValueBinding<float>(ModID, WeatherTime, Mod.m_Setting.WeatherTime);
            AddBinding(_currentWeatherTime);
            
            _currentFog = new ValueBinding<float>(ModID, Fog, Mod.m_Setting.Fog);
            AddBinding(_currentFog);
            
            _currentThunder = new ValueBinding<float>(ModID, Thunder, Mod.m_Setting.Thunder);
            AddBinding(_currentThunder);
            
            _currentRainbow = new ValueBinding<float>(ModID, Rainbow, Mod.m_Setting.Rainbow);
            AddBinding(_currentRainbow);
            
            _enableCustomTemperature = new ValueBinding<bool>(ModID, EnableCustomTemperature, Mod.m_Setting.EnableCustomTemperature);
            AddBinding(_enableCustomTemperature);
            
            _enableCustomFog = new ValueBinding<bool>(ModID, EnableCustomFog, Mod.m_Setting.EnableCustomFog);
            AddBinding(_enableCustomFog);
            
            _enableCustomThunder = new ValueBinding<bool>(ModID, EnableCustomThunder, Mod.m_Setting.EnableCustomThunder);
            AddBinding(_enableCustomThunder);
            
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

        private void SetCurrentTime(float time)
        {
            _currentOverrideTime.Update(time);
            Mod.m_Setting.Time = time;
            _timeAndWeatherControlSystem.UpdateTime();
        }
        
        private void SetCurrentTemperature(int temperature)
        {
            _currentOverrideTemperature.Update(temperature);
            Mod.m_Setting.Temperature = temperature;
            _timeAndWeatherControlSystem.UpdateTemperature();
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
            _timeAndWeatherControlSystem.UpdateSeason();
        }
        
        private void SetEnableCustomPrecipitation(bool enabled)
        {
            _enableCustomPrecipitation.Update(enabled);
            Mod.m_Setting.EnableCustomPrecipitation = enabled;
            _timeAndWeatherControlSystem.UpdatePrecipitation();
        }
        
        private void SetEnableCustomTemperature(bool enabled)
        {
            _enableCustomTemperature.Update(enabled);
            Mod.m_Setting.EnableCustomTemperature = enabled;
            _timeAndWeatherControlSystem.UpdateTemperature();
        }
        
        private void SetEnableCustomFog(bool enabled)
        {
            _enableCustomFog.Update(enabled);
            Mod.m_Setting.EnableCustomFog = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetEnableCustomThunder(bool enabled)
        {
            _enableCustomThunder.Update(enabled);
            Mod.m_Setting.EnableCustomThunder = enabled;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetEnableCustomClouds(bool enabled)
        {
            _enableCustomClouds.Update(enabled);
            Mod.m_Setting.EnableCustomClouds = enabled;
            _timeAndWeatherControlSystem.UpdateClouds();
        }
        
        private void SetEnableCustomAurora(bool enabled)
        {
            _enableCustomAurora.Update(enabled);
            Mod.m_Setting.EnableCustomAurora = enabled;
            _timeAndWeatherControlSystem.UpdateAurora();
        }
        
        private void SetCustomDayOfYear(int dayOfYear)
        {
            _currentDayOfTheYear.Update(dayOfYear);
            Mod.m_Setting.DayOfTheYear = dayOfYear;
            _timeAndWeatherControlSystem.UpdateTime();
        }
        
        private void SetCustomClouds(float clouds)
        {
            _currentClouds.Update(clouds);
            Mod.m_Setting.Clouds = clouds;
            _timeAndWeatherControlSystem.UpdateClouds();
        }
        
        private void SetCustomPrecipitation(float precipitation)
        {
            _currentPrecipitation.Update(precipitation);
            Mod.m_Setting.Precipitation = precipitation;
            _timeAndWeatherControlSystem.UpdatePrecipitation();
        }
        
        private void SetCustomAurora(float aurora)
        {
            _currentAurora.Update(aurora);
            Mod.m_Setting.Aurora = aurora;
            _timeAndWeatherControlSystem.UpdateAurora();
        }
        
        private void SetCustomWeatherTime(float time)
        {
            _currentWeatherTime.Update(time);
            Mod.m_Setting.WeatherTime = time;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomFog(float fog)
        {
            _currentFog.Update(fog);
            Mod.m_Setting.Fog = fog;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomThunder(float thunder)
        {
            _currentThunder.Update(thunder);
            Mod.m_Setting.Thunder = thunder;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
        
        private void SetCustomRainbow(float rainbow)
        {
            _currentRainbow.Update(rainbow);
            Mod.m_Setting.Rainbow = rainbow;
            _timeAndWeatherControlSystem.UpdateTimeAndWeather();
        }
    }
}
