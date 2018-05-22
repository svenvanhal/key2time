using System.Configuration;
using System.IO;

namespace Timetabling.Helper
{
    static class Util
    {

        // TODO: error handling
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Creates a directory in the temp folder.
        /// </summary>
        /// <param name="dirname">Name of the subdirectory.</param>
        /// <returns>Full path to the newly created directory.</returns>
        public static string CreateTempFolder(string dirname)
        {

            // TODO: make a global setting / const for the app directory
            var tempDirectory = Path.Combine(Path.GetTempPath(), "bachelorproject", dirname);
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

    }
}
