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
                { m_Setting.GetSettingsLocaleID(), "TimeWeatherAnarchy" },
                { m_Setting.GetOptionTabLocaleID(TimeWeatherAnarchySettings.kSection), "Main" },
            };
        }

        public void Unload()
        {

        }
    }
}
