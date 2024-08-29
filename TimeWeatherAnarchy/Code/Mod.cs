using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Input;
using Game.Modding;
using Game.SceneFlow;
using TimeWeatherAnarchy.Code.Settings;
using TimeWeatherAnarchy.Code.System;
using UnityEngine;

namespace TimeWeatherAnarchy.Code
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(TimeWeatherAnarchy)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static TimeWeatherAnarchySettings m_Setting { get; private set; }
        public static ProxyAction m_ButtonAction;
        public static ProxyAction m_AxisAction;
        public static ProxyAction m_VectorAction;

        public const string kButtonActionName = "ButtonBinding";
        public const string kAxisActionName = "FloatBinding";
        public const string kVectorActionName = "Vector2Binding";

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            m_Setting = new TimeWeatherAnarchySettings(this);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));

            m_Setting.RegisterKeyBindings();

            // Load mod settings
            AssetDatabase.global.LoadSettings(nameof(TimeWeatherAnarchy), m_Setting, new TimeWeatherAnarchySettings(this));

            // Load system
            updateSystem.UpdateAt<TimeControlSystem>(SystemUpdatePhase.MainLoop);
            updateSystem.UpdateAt<TimeControlSystem>(SystemUpdatePhase.ApplyTool);
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
