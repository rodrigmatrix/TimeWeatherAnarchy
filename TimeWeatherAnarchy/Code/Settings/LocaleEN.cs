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
        private readonly TimeWeatherAnarchySettings m_Setting;
        public LocaleEN(TimeWeatherAnarchySettings setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Time and Weather Anarchy" },
                { m_Setting.GetOptionTabLocaleID(TimeWeatherAnarchySettings.MainSection), "Main" },
                { m_Setting.GetOptionGroupLocaleID(TimeWeatherAnarchySettings.KeyBindingGroup), "Key Binding" },
                { m_Setting.GetOptionLabelLocaleID(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle)), "Open Time And Weather Anarchy" },
                { m_Setting.GetOptionDescLocaleID(nameof(TimeWeatherAnarchySettings.TriggerPanelToggle)), "Open or close the tool." },
            };
        }

        public void Unload()
        {

        }
    }
}