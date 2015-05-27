using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Common
{
    public class ConfigUtil
    {
        public static int GetValue(string key, int defaultValue)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(s))
                {
                    return defaultValue;
                }
                return int.Parse(s);
            }
            catch (Exception exception)
            {
                exception.ToString();
                return defaultValue;
            }
        }

        public static Int64 GetValue(string key, Int64 defaultValue)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(s))
                {
                    return defaultValue;
                }
                return Int64.Parse(s);
            }
            catch (Exception exception)
            {
                exception.ToString();
                return defaultValue;
            }
        }

        public static string GetValue(string key, string defaultValue)
        {
            try
            {
                string str = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(str))
                {
                    return defaultValue;
                }
                return str;
            }
            catch (Exception exception)
            {
                exception.ToString();
                return defaultValue;
            }
        }

        public static bool GetValue(string key, bool defaultValue)
        {
            try
            {
                string str = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(str))
                {
                    return defaultValue;
                }
                return bool.Parse(str);
            }
            catch (Exception exception)
            {
                exception.ToString();
                return defaultValue;
            }
        }
    }
}
