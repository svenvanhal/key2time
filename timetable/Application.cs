using System;
using System.IO;
using Timetabling.Algorithms;

namespace Timetabling
{

    /// <summary>
    /// Program entrypoint.
    /// </summary>
    public class Application
    {

        /// <summary>
        /// Temporary method used for development purposes.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            // Instantiate algorithm
            var fetPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Helper.Util.GetAppSetting("FetBinaryLocation"));
            var fetAlgo = new FetAlgorithm(fetPath);

            // Generate timetable
            fetAlgo.Execute("testdata/fet/United-Kingdom/Hopwood/Hopwood.fet");

#if DEBUG
            // Keep console window open
            Console.Read();
#endif
        }

    }
}
