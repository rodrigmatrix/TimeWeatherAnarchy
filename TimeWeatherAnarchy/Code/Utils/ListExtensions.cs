using System.Collections.Generic;

namespace TimeWeatherAnarchy.Code.Utils
{
    
    public static class ListExtension
    {
        public static T Next<T>(this List<T> list, T elt)
        {
            var index = list.IndexOf(elt);
            index = (index == list.Count - 1) ? 0 : index + 1;
            return list[index];
        }
        public static T Prev<T>(this List<T> list, T elt)
        {
            var index = list.IndexOf(elt);
            index = (index == 0) ? list.Count - 1 : index - 1;
            return list[index];
        }
    }
}
