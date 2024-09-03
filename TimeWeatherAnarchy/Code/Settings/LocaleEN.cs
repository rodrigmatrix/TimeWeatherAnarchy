using Colossal;
using Game.Input;
using Game.Settings;
using Game.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWeatherAnarchy.Code.Settings
{
    public class LocaleEN : IDictionarySource
    {
        private readonly TimeWeatherAnarchySettings _mSetting;
        public LocaleEN(TimeWeatherAnarchySettings setting)
        {
            _mSetting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { _mSetting.GetSettingsLocaleID(), "Time and Weather Anarchy" },
                
                { _mSetting.GetOptionTabLocaleID(TimeWeatherAnarchySettings.MainSection), "Main" },
                { _mSetting.GetOptionGroupLocaleID(TimeWeatherAnarchySettings.KeyBindingGroup), "Key Binding" },
                { _mSetting.GetOptionLabelLocaleID(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle)), "Open Time And Weather Anarchy" },
                { _mSetting.GetOptionLabelLocaleID(nameof(TimeWeatherAnarchySettings.TriggerDayNightToggle)), "Switch Day or Night Time Hotkey" },
                { _mSetting.GetOptionDescLocaleID(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle)), "Open or close the tool." },
                { _mSetting.GetOptionDescLocaleID(nameof(TimeWeatherAnarchySettings.TriggerDayNightToggle)), "Switch between day or night time when pressing this toggle." },
                { "TimeWeatherAnarchy.ModName", "Time and Weather Anarchy" },
                { "TimeWeatherAnarchy.ModIconTooltip", "Time and Weather Anarchy" },
                { "TimeWeatherAnarchy.Default", "Default" },
                { "TimeWeatherAnarchy.Custom", "Custom" },
                { "TimeWeatherAnarchy.TimeOptions", "Time Options" },
                { "TimeWeatherAnarchy.Day", "Day" },
                { "TimeWeatherAnarchy.Night", "Night" },
                { "TimeWeatherAnarchy.HourOfTheDay", "Hour of the day" },
                { "TimeWeatherAnarchy.DayOfTheYear", "Day of the year" },
                { "TimeWeatherAnarchy.WeatherOptions", "Weather Options" },
                { "TimeWeatherAnarchy.RainbowStrength", "Rainbow Strength" },
                { "TimeWeatherAnarchy.Spring", "Spring" },
                { "TimeWeatherAnarchy.Summer", "Summer" },
                { "TimeWeatherAnarchy.Fall", "Fall" },
                { "TimeWeatherAnarchy.Winter", "Winter" },
                { "TimeWeatherAnarchy.EnableCustomPrecipitation", "Enable Custom Precipitation" },
                { "TimeWeatherAnarchy.EnableCustomClouds", "Enable Custom Clouds" },
                { "TimeWeatherAnarchy.EnableCustomAurora", "Enable Custom Aurora" },
                { "TimeWeatherAnarchy.EnableCustomTemperature", "Enable Custom Temperature" },
                { "TimeWeatherAnarchy.EnableCustomFog", "Enable Custom Fog" },
                { "TimeWeatherAnarchy.EnableCustomThunder", "Enable Custom Thunder" },
                { "TimeWeatherAnarchy.WeatherDate", "Weather Date" },
                { "TimeWeatherAnarchy.Precipitation", "Precipitation" },
                { "TimeWeatherAnarchy.Clouds", "Clouds" },
                { "TimeWeatherAnarchy.Aurora", "Aurora" },
                { "TimeWeatherAnarchy.Temperature", "Temperature" },
                { "TimeWeatherAnarchy.Fog", "Fog" },
                { "TimeWeatherAnarchy.Thunder", "Thunder" },
                { "TimeWeatherAnarchy.Rainbow", "Rainbow" }
            };
        }

        public void Unload()
        {

        }
    }
}