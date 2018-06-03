﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
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
        public string OutputDir { get; protected set; }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IFileSystem _fs;
        private readonly string _baseDir;

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
            _fs = fileSystem;
            _baseDir = outputDir;

            InputName = inputName;
            OutputDir = GetOutputPath(outputDir);
        }

        /// <summary>
        /// Public facing method which processes the FET algorithm output.
        /// </summary>
        /// <returns>Timetable</returns>
        public Timetable GetTimetable()
        {

            Logger.Info("Looking for FET-CL activities output file in {0}.", OutputDir);

            Timetable tt;

            var outputPath = _fs.Path.Combine(OutputDir, $"{ InputName }_activities.xml");

            // Deserialize XML
            using (var outputFileStream = _fs.File.OpenRead(outputPath))
            {
                tt = XmlToTimetable(outputFileStream);
                Logger.Info($"Found a { (_partial ? "partial" : "full") } timetable with { tt.Activities.Count } activities in FET output.");
            }

            // Add meta data to timetable
            AddMetadata(tt);

            // Set partial flag for timetable
            tt.SetPartialFlag(_partial);

            // Clean up output dir
            //CleanupOutputDir();

            return tt;
        }

        /// <summary>
        /// Remove output generated by FET.
        /// </summary>
        public void CleanupOutputDir()
        {
            Logger.Info("Cleaning up output dir");

            // List all files in output directory
            _fs.Directory.Delete(_baseDir, true);
        }

        /// <summary>
        /// Deserializes an XML file to a Timetable object.
        /// </summary>
        /// <param name="fileStream">FET algorithm output XML file.</param>
        /// <returns>A Timetable object.</returns>
        /// <exception cref="SerializationException">XML serialization does not create a Timetable object.</exception>
        protected Timetable XmlToTimetable(Stream fileStream)
        {
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
        /// <param name="outputDir">Path to the FET output base directory.</param>
        /// <returns>Directory path to FET output files.</returns>
        protected string GetOutputPath(string outputDir)
        {
            var partialDir = _fs.Path.Combine(outputDir, $"{InputName}-highest");

            // Check if has partial results and set flag accordingly
            _partial = _fs.Directory.Exists(partialDir);
            return _partial ? partialDir : _fs.Path.Combine(OutputDir, InputName);
        }

        /// <summary>
        /// Parses the file describing the violated soft constraints for this timetable.
        /// </summary>
        /// <param name="tt">The timetable to add the soft conflict information to.</param>
        /// <returns>List of soft conflicts.</returns>
        protected Timetable AddMetadata(Timetable tt)
        {
            var softConstraintsFile = _fs.Path.Combine(OutputDir, $"{InputName}_soft_conflicts.txt");

            // Return original timetable if soft_constraints.txt does not exist
            if (!_fs.File.Exists(softConstraintsFile)) return tt;

            using (var reader = new StreamReader(softConstraintsFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    // Continue if empty, break when conflict list reached
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("Soft conflicts list") || line.StartsWith("Conflicts list")) break;

                    // Process line
                    ParseMetaLine(line, tt);
                }

                tt.SoftConflicts = ParseSoftConflicts(reader);
            }

            return tt;
        }

        /// <summary>
        /// Parse the warnings about violated soft constraints.
        /// </summary>
        /// <param name="reader">StreamReader to soft_conflicts.txt</param>
        /// <returns>List of violated soft constraints, per activity pair.</returns>
        protected List<string> ParseSoftConflicts(StreamReader reader)
        {
            var conflictList = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("End")) break;

                // Add conflict line to list
                conflictList.Add(line);
            }

            return conflictList;
        }

        /// <summary>
        /// Parses a soft_conflicts.txt line and sets updates the Timetable object with relevant information.
        /// </summary>
        /// <param name="line">Current line of the file</param>
        /// <param name="tt">Timetable object</param>
        protected void ParseMetaLine(string line, Timetable tt)
        {
            if (line.StartsWith("Total"))
            {
                // Total conflicts: __
                tt.ConflictWeight = Convert.ToDouble(Regex.Match(line, @"\d+(\.\d{1,2})?").Value);
            }
            else if (line.StartsWith("Warning! Only"))
            {
                // Warning! Only __ out of (total) activities placed!
                tt.PlacedActivities = Convert.ToUInt32(Regex.Match(line, @"\d+").Value);
            }
        }

    }
}
