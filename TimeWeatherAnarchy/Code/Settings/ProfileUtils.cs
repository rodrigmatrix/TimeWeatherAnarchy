using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Colossal.IO;
using Colossal.IO.AssetDatabase.Internal;
using Colossal.Json;
using Colossal.PSI.Environment;
using TimeWeatherAnarchy.Code.Domain;

namespace TimeWeatherAnarchy.Code.Settings;

public static class ProfileUtils
{
    
    private static readonly string ProfilesDirectory = Path.Combine(
        EnvPath.kUserDataPath,
        "ModsSettings",
        nameof(TimeWeatherAnarchy),
        "Profiles"
    );

    private static string GetDataDirectory() => ProfilesDirectory;

    private static string GetFilePath(string filename) => Path.Combine(GetDataDirectory(), filename);
        
    private static bool EnsureDataDirectory() => IOUtils.EnsureDirectory(GetDataDirectory());
    
    public static TimeWeatherProfile CreateDefault(TimeWeatherAnarchySettings settings)
    {
        TimeWeatherProfile profile;
        if (TryReadText(TimeWeatherProfile.DefaultID + ".json", out var fileString) && fileString.Length > 0)
        {
            profile = JSON.MakeInto<TimeWeatherProfile>(JSON.Load(fileString));
        }
        else
        {
            profile = new TimeWeatherProfile
            {
                Id = TimeWeatherProfile.DefaultID,
                Index = 0,
                Name = "Main",
                WeatherOption = settings.WeatherOption,
                Temperature = settings.Temperature,
                Fog = settings.Fog,
                Thunder = settings.Thunder,
                TimeOption = settings.TimeOption,
                Time = settings.Time,
                WeatherTime = settings.WeatherTime,
                EnableCustomPrecipitation = settings.EnableCustomPrecipitation,
                EnableCustomTemperature = settings.EnableCustomTemperature,
                EnableCustomClouds = settings.EnableCustomClouds,
                EnableCustomAurora = settings.EnableCustomAurora,
                EnableCustomFog = settings.EnableCustomFog,
                EnableCustomThunder = settings.EnableCustomThunder,
                Rainbow = settings.Rainbow,
                Aurora = settings.Aurora,
                Clouds = settings.Clouds,
                Precipitation = settings.Precipitation,
                DayOfTheYear = settings.DayOfTheYear
            };
        }
        return profile;
    }
    
     public static List<TimeWeatherProfile> LoadProfiles(TimeWeatherAnarchySettings settings)
        {
            var dir = new DirectoryInfo(GetDataDirectory());
            var profiles = new List<TimeWeatherProfile>();
            if (dir.Exists)
            {
                var files = dir.GetFiles("*.json");
                files.ForEach(fileInfo =>
                {
                    if (fileInfo.DirectoryName == null) return;
                    try
                    {
                        var text = File.ReadAllText(fileInfo.FullName);
                        var profile = JSON.MakeInto<TimeWeatherProfile>(JSON.Load(text));
                        profiles.Add(profile);
                    }
                    catch (Exception e)
                    {
                        Mod.log.Info("error loading profile. check json for issues " + e.Message);
                    }
                });
                if (profiles.FirstOrDefault(profile => profile.Id == TimeWeatherProfile.DefaultID) == null)
                {
                    var defaultProfile = CreateDefault(settings);
                    profiles.Add(defaultProfile);
                    Save(defaultProfile);
                }
            }
            else
            {
                var defaultProfile = CreateDefault(settings);
                profiles.Add(defaultProfile);
                Save(defaultProfile);
            }
            return profiles.OrderBy(q => q.Index).ToList();
        }

        public static void Delete(string id)
        {
            var file = GetFilePath(id + ".json");
            if (id.Contains("default_profile"))
            {
                return;
            }
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        private static void SaveText(string filename, string text)
        {
            try
            {
                File.WriteAllText(GetFilePath(filename), text);
            }
            catch (Exception e)
            {
                Mod.log.Error(e, $"Failed to write profile {filename}");
            }
        }

        private static bool TryReadText(string filename, out string text) {
            var path = GetFilePath(filename);
            if (File.Exists(path)) {
                text = File.ReadAllText(path);
                return true;
            }
            Mod.log.Info($"Tried to read {filename}, but it does not exist.");
            text = "";
            return false;
        }

        public static void Save(TimeWeatherProfile profile)
        {
            EnsureDataDirectory();
            SaveText(profile.Id + ".json", JSON.Dump(profile));
        }
}