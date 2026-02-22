using System.Collections.Generic;
using System.IO;
using Colossal.IO;
using Colossal.IO.AssetDatabase.Internal;
using Colossal.Json;
using Colossal.PSI.Environment;

namespace TimeWeatherAnarchy.Code.Settings
{
    public static class SaveLinkUtils
    {
        private static readonly string FilePath = Path.Combine(
            EnvPath.kUserDataPath,
            "ModsSettings",
            nameof(TimeWeatherAnarchy),
            "save_links.json"
        );

        public static Dictionary<string, string> Load()
        {
            if (!File.Exists(FilePath))
                return new Dictionary<string, string>();
            try
            {
                var json = File.ReadAllText(FilePath);
                return JSON.MakeInto<Dictionary<string, string>>(JSON.Load(json))
                       ?? new Dictionary<string, string>();
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }

        public static void Save(Dictionary<string, string> links)
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(FilePath, JSON.Dump(links));
        }
    }
}
