using Colossal.UI.Binding;
using Game.UI;
using System;

namespace TimeWeatherAnarchy.Code.System
{
    internal partial class TimeWeatherAnarchyUISystem : UISystemBase
    {
        private TimeControlSystem timeControlSystem;
        
        private const string MOD_ID = "TimeWeatherAnarchyUI";
        private const string MAIN_PANNEL_OPEN = "MainPannelOpen";
        private const string CURRENT_TIME = "CurrentTime";
        private const string CURRENT_TEMPERATURE = "CurrentTemperature";
        private const string TIME_OPTION = "TimeOption";
        private const string WEATHER_OPTION = "WeatherOption";
        
        private ValueBinding<bool> panelVisibleBinding;
        private ValueBinding<int> currentOverrideTime;
        private ValueBinding<int> currentOverrideTemperature;
        private ValueBinding<int> timeOption;
        private ValueBinding<int> weatherOption;

        protected override void OnCreate()
        {
            base.OnCreate();
            timeControlSystem = World.GetOrCreateSystemManaged<TimeControlSystem>();
            
            // get bindings

            panelVisibleBinding = new ValueBinding<bool>(MOD_ID, MAIN_PANNEL_OPEN, false);
            AddBinding(panelVisibleBinding);

            currentOverrideTime = new ValueBinding<int>(MOD_ID, CURRENT_TIME, Mod.m_Setting.CurrentTime);
            AddBinding(currentOverrideTime);
            
            currentOverrideTemperature = new ValueBinding<int>(MOD_ID, CURRENT_TEMPERATURE, 0);
            AddBinding(currentOverrideTemperature);

            
            timeOption = new ValueBinding<int>(MOD_ID, TIME_OPTION, 0);
            AddBinding(timeOption);
            
            weatherOption = new ValueBinding<int>(MOD_ID, WEATHER_OPTION, 0);
            AddBinding(weatherOption);
            
            // set bindings

            AddBinding(new TriggerBinding<bool>(MOD_ID, MAIN_PANNEL_OPEN, SetPanelVisibility));

            AddBinding(new TriggerBinding<int>(MOD_ID, CURRENT_TIME, SetCurrentTime));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, CURRENT_TEMPERATURE, SetCurrentTemperature));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, TIME_OPTION, SetTimeOption));
            
            AddBinding(new TriggerBinding<int>(MOD_ID, WEATHER_OPTION, SetWeatherOption));
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        private void SetPanelVisibility(bool open)
        {
            panelVisibleBinding.Update(open);
        }

        private void SetCurrentTime(int time)
        {
            currentOverrideTime.Update(time);
            Mod.m_Setting.CurrentTime = time;
            timeControlSystem.UpdateTime();
        }
        
        private void SetCurrentTemperature(int temperature)
        {
            currentOverrideTemperature.Update(temperature);
            Mod.m_Setting.CurrentTemperature = temperature;
            timeControlSystem.UpdateWeather();
        }
        
        private void SetTimeOption(int option)
        {
            timeOption.Update(option);
            Mod.m_Setting.TimeOption = option;
            timeControlSystem.UpdateTime();
        }
        
        private void SetWeatherOption(int option)
        {
            weatherOption.Update(option);
            Mod.m_Setting.WeatherOption = option;
            timeControlSystem.UpdateWeather();
        }
    }
}
