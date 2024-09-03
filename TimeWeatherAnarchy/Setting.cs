using Colossal.IO.AssetDatabase;
using Game.Input;
using Game.Modding;
using Game.Settings;

namespace TimeWeatherAnarchy
{
    
    [FileLocation("ModsSettings/" + nameof(TimeWeatherAnarchy))]
    [SettingsUIGroupOrder(KeyBindingGroup)]
    [SettingsUIShowGroupName(KeyBindingGroup)]
    public class TimeWeatherAnarchySettings(IMod mod) : ModSetting(mod)
    {
        public const string MainSection = "Main";

        public const string KeyBindingGroup = "KeyBindingGroup";


        [SettingsUISection(MainSection, KeyBindingGroup), SettingsUIKeyboardBinding]
        public ProxyBinding TriggerPanelToggle { get; set; }
        
        [SettingsUISection(MainSection, KeyBindingGroup), SettingsUIKeyboardBinding]
        public ProxyBinding TriggerDayNightToggle { get; set; }

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
