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
                { _mSetting.GetOptionLabelLocaleID(nameof(TimeWeatherAnarchySettings.TriggerNextProfileToggle)), "Go to the next settings profile Hotkey" },
                { _mSetting.GetOptionDescLocaleID(nameof(TimeWeatherAnarchySettings.TriggerNextProfileToggle)), "Change your settings profile to the next one." },
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
                { "TimeWeatherAnarchy.Cancel", "Cancel" },
                { "TimeWeatherAnarchy.Edit", "Edit" },
                { "TimeWeatherAnarchy.Save", "Save" },
                { "TimeWeatherAnarchy.CopyCurrentProfile", "Copy preferences from current profile" },
                { "TimeWeatherAnarchy.ProfileOptionsDescription", "Create profiles that save all your preferences and select them" },
                {"TimeWeatherAnarchy.ProfileOptions", "Profiles" },
                {"TimeWeatherAnarchy.Create", "Create" },
                {"TimeWeatherAnarchy.NewProfile", "New Profile" },
                {"TimeWeatherAnarchy.Latitude", "Sun Latitude" },
                {"TimeWeatherAnarchy.Longitude", "Sun Longitude" },
                {"TimeWeatherAnarchy.Delete", "Delete" },
                {"TimeWeatherAnarchy.CreateProfile", "Create Profile" },
                {"TimeWeatherAnarchy.UpdateProfile", "Update Profile" },
                {"TimeWeatherAnarchy.EditProfile", "Edit Profile" },
                {"TimeWeatherAnarchy.EditingProfile", "Editing Profile" },
                {"TimeWeatherAnarchy.TypeProfileName", "Profile Name..." },
                {"TimeWeatherAnarchy.TypeUpdatedProfileName", "Updated Profile Name..." },
                { "TimeWeatherAnarchy.DeleteProfileConfirmation", "Are you sure you want to delete this profile?" },
                { "TimeWeatherAnarchy.DeleteProfile", "Delete Profile" },
            };
        }

        public void Unload()
        {

        }
    }
}