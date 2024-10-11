using System;
using System.Collections.Generic;
using System.Linq;
using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Game.Input;
using Game.Modding;
using Game.Settings;
using TimeWeatherAnarchy.Code.Domain;
using TimeWeatherAnarchy.Code.Settings;
using TimeWeatherAnarchy.Code.Utils;

namespace TimeWeatherAnarchy
{
    
    [FileLocation("ModsSettings/" + nameof(TimeWeatherAnarchy))]
    [SettingsUIGroupOrder(KeyBindingGroup)]
    [SettingsUIShowGroupName(KeyBindingGroup)]
    public class TimeWeatherAnarchySettings(IMod mod) : ModSetting(mod)
    {
        public const string MainSection = "Main";

        public const string KeyBindingGroup = "KeyBindingGroup";
        
        [Exclude]
        public TimeWeatherProfile Profile => Profiles.Find((p) => p.Id == SelectedProfile) ?? ProfileUtils.CreateDefault(this);
        
        [Exclude]
        public List<TimeWeatherProfile> Profiles { get; private set; } = [];

        public void InitializeProfiles()
        {
            var profiles = ProfileUtils.LoadProfiles(this);
            Profiles = profiles;
        }
        
        public void CreateProfile(string profileName, bool copyCurrentProfile)
        {
            if (copyCurrentProfile)
            {
                var profile = Profile;
                profile.Id = Guid.NewGuid().ToString();
                profile.Name = profileName;
                profile.Index = Profiles.Count;
                ProfileUtils.Save(profile);
                SelectedProfile = profile.Id;
            }
            else
            {
                var profile = TimeWeatherProfile.Create(profileName, Profiles.Count);
                ProfileUtils.Save(profile);
                SelectedProfile = profile.Id;
            }
            InitializeProfiles();
        }
        
        public void UpdateProfile(string profileId, string profileName)
        {
            var profile = Profiles.Find((p) => p.Id == profileId);
            profile.Name = profileName;
            Profiles.Add(profile);
            SelectedProfile = profile.Id;
            ProfileUtils.Save(profile);
            InitializeProfiles();
        }
        
        public void DeleteProfile(string profileId)
        {
            var profile = Profiles.Find((p) => p.Id == profileId);
            SelectedProfile = Profiles.Prev(profile).Id;
            ProfileUtils.Delete(profileId);
            InitializeProfiles();
        }

        [SettingsUISection(MainSection, KeyBindingGroup), SettingsUIKeyboardBinding]
        public ProxyBinding TriggerPanelToggle { get; set; }
        
        [SettingsUISection(MainSection, KeyBindingGroup), SettingsUIKeyboardBinding]
        public ProxyBinding TriggerDayNightToggle { get; set; }
        
        [SettingsUISection(MainSection, KeyBindingGroup), SettingsUIKeyboardBinding]
        public ProxyBinding TriggerNextProfileToggle { get; set; }
        
        [SettingsUIHidden]
        public string SelectedProfile { get; set; } = TimeWeatherProfile.DefaultID;
        
        [SettingsUIHidden]
        public float Time { get; set; }
        
        [SettingsUIHidden]
        public float WeatherTime { get; set; }
        
        [SettingsUIHidden]
        public int Temperature { get; set; }
        
        [SettingsUIHidden]
        public float Fog { get; set; }
        
        [SettingsUIHidden]
        public float Thunder { get; set; }
        
        [SettingsUIHidden]
        public int TimeOption { get; set; }
        
        [SettingsUIHidden]
        public int WeatherOption { get; set; }
        
        [SettingsUIHidden]
        public bool EnableCustomPrecipitation { get; set; }
        
        [SettingsUIHidden]
        public bool EnableCustomTemperature { get; set; }
        
        [SettingsUIHidden]
        public bool EnableCustomClouds { get; set; }
        
        [SettingsUIHidden]
        public bool EnableCustomAurora { get; set; }
        
        [SettingsUIHidden]
        public bool EnableCustomFog { get; set; }
        
        [SettingsUIHidden]
        public bool EnableCustomThunder { get; set; }
        
        [SettingsUIHidden]
        public float Rainbow { get; set; }
        
        [SettingsUIHidden]
        public float Aurora { get; set; }
        
        [SettingsUIHidden]
        public float Clouds { get; set; }
        
        [SettingsUIHidden]
        public float Precipitation { get; set; }
        
        [SettingsUIHidden]
        public int DayOfTheYear { get; set; }

        public override void SetDefaults()
        {
            //throw new System.NotImplementedException();
        }
    }

}
