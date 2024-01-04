using System;

namespace Utils
{
    public static class Utils
    {
        public static string GetCurrentLocalTime => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}