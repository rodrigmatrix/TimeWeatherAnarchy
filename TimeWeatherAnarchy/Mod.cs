using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using TimeWeatherAnarchy.Code.Settings;
using TimeWeatherAnarchy.Code.System;

namespace TimeWeatherAnarchy
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(TimeWeatherAnarchy)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static TimeWeatherAnarchySettings m_Setting { get; private set; }

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            m_Setting = new TimeWeatherAnarchySettings(this);
            m_Setting.RegisterKeyBindings();
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));

            // Load mod settings
            AssetDatabase.global.LoadSettings(
                nameof(TimeWeatherAnarchy),
                m_Setting,
                new TimeWeatherAnarchySettings(this)
            );
            

            // Load system
            updateSystem.UpdateAt<TimeAndWeatherControlSystem>(SystemUpdatePhase.MainLoop);
            updateSystem.UpdateAt<TimeWeatherAnarchyUISystem>(SystemUpdatePhase.UIUpdate);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
