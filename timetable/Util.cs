using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

    }
}
