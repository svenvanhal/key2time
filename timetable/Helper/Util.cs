using System;
using System.Configuration;
using System.IO;

namespace Timetabling.Helper
{
    static class Util
    {

        /// <summary>
        /// Todo: remove and set-up full-featured logger.
        /// </summary>
        public static void WriteError(string error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
            Console.ResetColor();
        }

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
            string tempDirectory = Path.Combine(Path.GetTempPath(), "bachelorproject", dirname);
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

    }
}
