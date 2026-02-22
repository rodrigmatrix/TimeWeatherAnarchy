using System.Collections.Generic;
using System.IO;
using Colossal.IO;
using Colossal.IO.AssetDatabase.Internal;
using Colossal.Json;
using Colossal.PSI.Environment;

namespace TimeWeatherAnarchy.Code.Settings
{
    public class SaveLinkData
    {
        public string ProfileId { get; set; }
        public string DisplayName { get; set; }
    }

    public static class SaveLinkUtils
    {
        private static readonly string LinksPath = Path.Combine(
            EnvPath.kUserDataPath, "ModsSettings", nameof(TimeWeatherAnarchy), "save_links.json");

        private static Dictionary<string, SaveLinkData> LoadFile(string path)
        {
            if (!File.Exists(path)) return new Dictionary<string, SaveLinkData>();
            try
            {
                var json = File.ReadAllText(path);
                return JSON.MakeInto<Dictionary<string, SaveLinkData>>(JSON.Load(json))
                       ?? new Dictionary<string, SaveLinkData>();
            }
            catch
            {
                return new Dictionary<string, SaveLinkData>();
            }
        }

        private static void SaveFile(string path, Dictionary<string, SaveLinkData> dict)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(path, JSON.Dump(dict));
        }

        public static Dictionary<string, SaveLinkData> LoadLinks() => LoadFile(LinksPath);
        public static void SaveLinks(Dictionary<string, SaveLinkData> links) => SaveFile(LinksPath, links);
    }
}
