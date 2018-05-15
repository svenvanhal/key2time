using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Timetabling.Helper
{

    /// <summary>
    /// Construct a name / value collection for use at the command line.
    /// </summary>
    public class CommandLineArguments : Dictionary<string, string>
    {

        /// <summary>
        /// Combine two CommandLineArguments objects. The values of the second collection take precedence.
        /// </summary>
        /// <param name="other">The CommandLineArguments object to be merged into the current object.</param>
        public CommandLineArguments Combine(CommandLineArguments other)
        {

            // Merge CommandLineArguments. Uses the indexer (instead of this.Add) to allow overwriting existing keys.
            foreach (KeyValuePair<string, string> entry in other)
            {
                this[entry.Key] = entry.Value;
            }

            return this;

        }

        /// <summary>
        /// Encodes arguments for command-line usage.
        /// </summary>
        /// <param name="original">Original arguments.</param>
        /// <returns>Encoded arguments.</returns>
        /// <remarks><a href="https://stackoverflow.com/a/12364234">Source</a></remarks>
        public static string EncodeArgument(string original)
        {

            // Empty argument
            if (string.IsNullOrEmpty(original))
            {
                return "\"\"";
            }

            string value = Regex.Replace(original, @"(\\*)" + "\"", @"$1\$0");
            return Regex.Replace(value, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"", RegexOptions.Singleline);
        }

    }

}
