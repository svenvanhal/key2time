using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Timetable
{
    class Util
    {

        /// <summary>
        /// Encodes arguments for command-line usage.
        /// </summary>
        /// <param name="original">Original arguments.</param>
        /// <returns>Encoded arguments.</returns>
        /// <remarks><a href="https://stackoverflow.com/a/12364234">Source</a></remarks>
        public static string EncodeParameterArgument(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return original;
            }

            string value = Regex.Replace(original, @"(\\*)" + "\"", @"$1\$0");
            return Regex.Replace(value, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"", RegexOptions.Singleline);
        }

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

    }
}
