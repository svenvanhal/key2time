using System;
using System.Collections.Specialized;
using System.Configuration;
using Timetabling.Algorithms.FET;

namespace Timetabling.Config
{

    /// <summary>
    /// Timetabling library configuration class. Retrieves timetabling settings from app.config.
    /// </summary>
    public sealed class TimetablingConfig
    {

        /// <summary>
        /// Timetabling configuration object.
        /// </summary>
        public static TimetablingConfig Instance => ConfigInstance.Value;

        private static readonly Lazy<TimetablingConfig> ConfigInstance = new Lazy<TimetablingConfig>(() => new TimetablingConfig());
        private readonly NameValueCollection _configCollection;

        private TimetablingConfig() => _configCollection = ConfigurationManager.GetSection("timetablingSettings") as NameValueCollection;

        /// <summary>
        /// Get a Timetabling configuration setting.
        /// </summary>
        /// <param name="key">Setting key.</param>
        /// <returns>Setting value.</returns>
        public static string GetSetting(string key) => Instance._configCollection[key];

        /// <summary>
        /// Get Timeout setting.
        /// </summary>
        /// <returns>FetTimout setting.</returns>
        public static int GetTimeout(int defaultValue = 0)
        {
            var setting = GetSetting("Timeout");
            return setting == null ? defaultValue : int.Parse(setting);
        }

        /// <summary>
        /// Get FetExecutableLocation setting.
        /// </summary>
        /// <returns>Location of FET-CL executable.</returns>
        public static string GetFetExecutableLocation(string defaultValue = "lib/fet/fet-cl")
        {
            var setting = GetSetting("FetExecutableLocation");
            return setting ?? defaultValue;
        }

        /// <summary>
        /// Get FetLanguage setting.
        /// </summary>
        /// <returns>Language for FET-CL output.</returns>
        public static FetLanguage GetFetLanguage(FetLanguage defaultValue = null)
        {
            if(defaultValue == null) defaultValue = FetLanguage.US_English;
            var setting = GetSetting("FetExecutableLocation");

            // Return default value if setting not set
            if (setting == null) return defaultValue;

            var languageType = Type.GetType(setting);

            // Return FetLanguage from setting (if exists), else return default value
            return languageType != null ? Activator.CreateInstance(languageType) as FetLanguage : defaultValue;
        }

        /// <summary>
        /// Get FetWorkingDir setting.
        /// </summary>
        /// <returns>Language for FET-CL output.</returns>
        public static string GetFetWorkingDir(string defaultValue = @"%TEMP%\Timetabling")
        {
            var setting = GetSetting("FetWorkingDir");
            return setting ?? defaultValue;
        }

    }
}
