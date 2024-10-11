using System.Collections.Generic;

namespace TimeWeatherAnarchy.Code.Domain;

public static class TimeWeatherProfileExtension
{
    public static TimeWeatherProfileUI ToUI(this TimeWeatherProfile profile)
    {
        return new TimeWeatherProfileUI
        {
            Id = profile.Id,
            Index = profile.Index,
            Name = profile.Name
        };
    }
    
    public static List<TimeWeatherProfileUI> ToUI(this List<TimeWeatherProfile> profiles)
    {
        return profiles.ConvertAll(profile => profile.ToUI());
    }
}