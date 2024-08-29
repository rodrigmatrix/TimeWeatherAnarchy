using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Input;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TimeWeatherAnarchy.Code.System;

namespace TimeWeatherAnarchy.Code.Settings
{
    [FileLocation("ModsSettings/" + nameof(TimeWeatherAnarchy) + "/" + nameof(TimeWeatherAnarchy))]
    [SettingsUIGroupOrder(kTimeGroup, kWeatherGroup, kDateGroup, kResetGroup)]
    [SettingsUIShowGroupName(kTimeGroup, kWeatherGroup, kDateGroup, kResetGroup)]
    public class TimeWeatherAnarchySettings : ModSetting
    {
        public const string kSection = "Main";

        public const string kTimeGroup = "Time Controls";
        public const string kWeatherGroup = "Weather Controls";
        public const string kDateGroup = "Date Controls";
        public const string kResetGroup = "Reset Controls";

        // current prefs
        private int currentTime = 0;
        private bool setTimeManually = false;

        public TimeWeatherAnarchySettings(IMod mod) : base(mod)
        {

        }

        [SettingsUIHidden]
        [SettingsUISection(kTimeGroup)]
        public int CurrentTime { get; set; }
        
        [SettingsUIHidden]
        [SettingsUISection(kTimeGroup)]
        public int CurrentTemperature { get; set; }
        
        [SettingsUIHidden]
        [SettingsUISection(kTimeGroup)]
        public int TimeOption { get; set; }
        
        [SettingsUIHidden]
        [SettingsUISection(kTimeGroup)]
        public int WeatherOption { get; set; }

        [SettingsUISection(kSection, kResetGroup)]
        public bool ResetBindings
        {
            set
            {
                Mod.log.Info("Reset key bindings");
                ResetKeyBindings();
            }
        }

        public override void SetDefaults()
        {
            //throw new System.NotImplementedException();
        }

       
    }

}
