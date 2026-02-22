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
        private static readonly string LinksPath = Path.Combine(
            EnvPath.kUserDataPath, "ModsSettings", nameof(TimeWeatherAnarchy), "save_links.json");

        private static readonly string NamesPath = Path.Combine(
            EnvPath.kUserDataPath, "ModsSettings", nameof(TimeWeatherAnarchy), "save_names.json");

        private static Dictionary<string, string> LoadFile(string path)
        {
            if (!File.Exists(path)) return new Dictionary<string, string>();
            try
            {
                var json = File.ReadAllText(path);
                return JSON.MakeInto<Dictionary<string, string>>(JSON.Load(json))
                       ?? new Dictionary<string, string>();
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }

        private static void SaveFile(string path, Dictionary<string, string> dict)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(path, JSON.Dump(dict));
        }

        public static Dictionary<string, string> LoadLinks() => LoadFile(LinksPath);
        public static Dictionary<string, string> LoadNames() => LoadFile(NamesPath);

        public static void SaveLinks(Dictionary<string, string> links) => SaveFile(LinksPath, links);
        public static void SaveNames(Dictionary<string, string> names) => SaveFile(NamesPath, names);
    }
}
