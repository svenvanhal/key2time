﻿using System.IO;
using System.IO.Abstractions;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Timetabling.Resources;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// Processes the FET output.
    /// </summary>
    public class FetOutputProcessor
    {

        /// <summary>
        /// Name of the FET input. The InputName is primarily used as the name of the FET output directory.
        /// </summary>
        public string InputName { get; }

        /// <summary>
        /// The path to the FET output.
        /// </summary>
        public string OutputDir { get; }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IFileSystem _fs;

        /// <inheritdoc />
        public FetOutputProcessor(string inputName, string outputDir) : this(inputName, outputDir, new FileSystem()) { }

        private bool _partial;

        /// <summary>
        /// Create new FetOutputProcessor.
        /// </summary>
        /// <param name="inputName">Name of the FET file used for the program input.</param>
        /// <param name="outputDir">Location of the FET output files.</param>
        /// <param name="fileSystem">Filesystem to use.</param>
        internal FetOutputProcessor(string inputName, string outputDir, IFileSystem fileSystem)
        {
            InputName = inputName;
            OutputDir = outputDir;
            _fs = fileSystem;
        }

        /// <summary>
        /// Public facing method which processes the FET algorithm output.
        /// </summary>
        /// <returns>Timetable</returns>
        public Timetable GetTimetable()
        {

            Logger.Info("Looking for FET-CL activities output file in {0}.", OutputDir);

            Timetable tt;

            var outputPath = _fs.Path.Combine(GetOutputPath(), $"{ InputName }_activities.xml");

            // Deserialize XML
            using (var outputFileStream = _fs.File.OpenRead(outputPath))
            {
                tt = XmlToTimetable(outputFileStream);
                Logger.Info($"Found a { (_partial ? "partial" : "full") } timetable with { tt.Activities.Count } activities in FET output.");
            }

            // Set partial flag for timetable
            tt.SetPartialFlag(_partial);

            // Clean up output dir
            CleanupOutputDir();

            return tt;
        }

        /// <summary>
        /// Remove output generated by FET.
        /// </summary>
        public void CleanupOutputDir()
        {
            Logger.Info("Cleaning up output dir");

            // List all files in output directory
            _fs.Directory.Delete(OutputDir, true);
        }

        /// <summary>
        /// Deserializes an XML file to a Timetable object.
        /// </summary>
        /// <param name="fileStream">FET algorithm output XML file.</param>
        /// <returns>A Timetable object.</returns>
        /// <exception cref="SerializationException">XML serialization does not create a Timetable object.</exception>
        public Timetable XmlToTimetable(Stream fileStream)
        {

            // Initialize
            var serializer = new XmlSerializer(typeof(Timetable));

            // Read and deserialize XML
            using (var reader = XmlReader.Create(fileStream))
            {
                return serializer.Deserialize(reader) as Timetable;
            }

        }

        /// <summary>
        /// Determine the output path to the timetable files.
        /// </summary>
        /// <returns>Directory path to FET output files.</returns>
        protected string GetOutputPath()
        {
            // Check if has partial results
            var partialDir = _fs.Path.Combine(OutputDir, $"{InputName}-highest");

            // Return partial dir (suffix -highest) when exists
            if (_fs.Directory.Exists(partialDir))
            {
                _partial = true;
                return partialDir;
            }

            return _fs.Path.Combine(OutputDir, InputName);
        }

    }
}
