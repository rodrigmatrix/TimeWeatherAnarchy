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

        [SettingsUIHidden]
        public int CurrentTime { get; set; }
        
        [SettingsUIHidden]
        public float CurrentWeatherTime { get; set; }
        
        [SettingsUIHidden]
        public int CurrentTemperature { get; set; }
        
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
        public float CurrentAurora { get; set; }
        
        [SettingsUIHidden]
        public float CurrentClouds { get; set; }
        
        [SettingsUIHidden]
        public float CurrentPrecipitation { get; set; }
        
        [SettingsUIHidden]
        public int CurrentDayOfTheYear { get; set; }

        public override void SetDefaults()
        {
            //throw new System.NotImplementedException();
        }
    }

}
