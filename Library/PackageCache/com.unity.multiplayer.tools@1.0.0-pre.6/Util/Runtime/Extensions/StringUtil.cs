using System.Linq;

namespace Unity.Multiplayer.Tools.Util
{
    internal static class StringUtil
    {
        internal static string AddSpacesToCamelCase(string s)
        {
            return string.Concat(s.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }
}