using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Timetabling.Helper
{

    /// <inheritdoc />
    /// <summary>
    /// Construct a name / value collection for use at the command line.
    /// </summary>
    public class CommandLineArguments : Dictionary<string, string>
    {

        /// <summary>
        /// Different command line argument outputs.
        /// </summary>
        public enum OutputStyle
        {

            /// <summary>
            /// Formats arguments as follows: --name=value
            /// </summary>
            DoubleDashEquals,

            /// <summary>
            /// Formats arguments as follows: -name=value
            /// </summary>
            DashEquals,

            /// <summary>
            /// Formats arguments as follows: /name:value
            /// </summary>
            SlashColon,

            /// <summary>
            /// Formats arguments as follows: /name=value
            /// </summary>
            SlashEquals,

            /// <summary>
            /// Formats arguments as follows: /name value
            /// </summary>
            SlashSpace,

        };

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
        /// Create string representation of arguments.
        /// </summary>
        /// <returns>String representation of arguments.</returns>
        public override string ToString()
        {

            return ToString(OutputStyle.DoubleDashEquals);

        }


        /// <summary>
        /// Create string representation based on a predefined style template.
        /// </summary>
        /// <param name="style"><see cref="OutputStyle"/></param>
        /// <returns>String representation of CommandLineArguments.</returns>
        public string ToString(OutputStyle style)
        {

            var sb = new StringBuilder();
            foreach (var arg in this)
            {

                // Append each argument based on the chosen style template
                sb.AppendFormat(
                    GetOutputStyleTemplate(style),
                    EncodeArgument(arg.Key),
                    EncodeArgument(arg.Value)
                );
            }
            return sb.ToString();

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

        /// <summary>
        /// Create string template from <see cref="OutputStyle"/>
        /// </summary>
        /// <param name="style"><see cref="OutputStyle"/></param>
        /// <returns>Argument string template.</returns>
        private static string GetOutputStyleTemplate(OutputStyle style)
        {

            switch (style)
            {
                default: // OutputStyle.DoubleDashEquals
                    return " --{0}={1}";

                case OutputStyle.DashEquals:
                    return " -{0}={1}";

                case OutputStyle.SlashColon:
                    return " /{0}:{1}";

                case OutputStyle.SlashEquals:
                    return " /{0}={1}";

                case OutputStyle.SlashSpace:
                    return " /{0} {1}";
            }

        }

    }

}
