using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Xml.Linq;
using Timetabling.Resources;
using Timetabling.Resources.Constraints;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// Class responsibility:
    ///  - Take generic objects
    ///  - Convert to XML
    ///  - Write XML to disk and return path
    /// </summary>
    public class FetInputGenerator
    {

        private readonly IFileSystem _fs;

        /// <inheritdoc />
        public FetInputGenerator() : this(new FileSystem()) { }

        /// <summary>
        /// Create new FetInputGenerator.
        /// </summary>
        /// <param name="fileSystem">Filesystem to use.</param>
        internal FetInputGenerator(IFileSystem fileSystem) => _fs = fileSystem;

        public string ToFile(XDocument doc, string path)
        {
            using (var fileStream = _fs.File.OpenWrite(path))
            {
                doc.Save(fileStream, SaveOptions.DisableFormatting);
            }

            return path;
        }

        /// <summary>
        /// Serialize Timetabling resources to FET-compatible XML file.
        /// </summary>
        /// <param name="resources">Timetabling resources.</param>
        public XDocument BuildXml(TimetableResourceCollection resources)
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));

            // Create XDocument
            var document = new XDocument();
            var root = new XElement("fet", new XAttribute("version", "5.35.7"));

            // Build FET XML structure
            document.Add(root);
            root.Add(SerializeDays(resources.Days));
            root.Add(SerializeTimeslots(resources.Timeslots));
            root.Add(Serialize("Subjects_List", resources.Subjects));
            root.Add(Serialize("Teachers_List", resources.Teachers));
            root.Add(Serialize("Students_List", resources.Students));
            root.Add(Serialize("Activities_List", resources.Activities));
            root.Add(Serialize("Rooms_List", resources.Rooms));
            root.Add(SerializeConstraints("Time_Constraints_List", resources.TimeConstraints));
            root.Add(SerializeConstraints("Space_Constraints_List", resources.SpaceConstraints));

            Console.Write(document.ToString());

            return document;
        }

        /// <summary>
        /// Serializes a subclass of Resource to XML.
        /// N.B.: The element type is only checked on runtime, so FetSerializer.Serialize() MUST have an overload for all subclasses of Resource to prevent any unexpected exceptions.
        /// </summary>
        /// <typeparam name="T">Type of the input resource element</typeparam>
        /// <param name="elementName">Name of the containing element</param>
        /// <param name="resources">Timetabling resources</param>
        /// <returns>An XElement.</returns>
        protected XElement Serialize<T>(string elementName, Dictionary<int, T> resources) where T : Resource
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));
            var container = new XElement(elementName);

            // Add elements
            foreach (var element in resources) { container.Add(FetSerializer.Serialize((dynamic)element.Value)); }

            return container;
        }

        /// <summary>
        /// Serializes a subclass of AbstractConstraint to XML.
        /// N.B.: The element type is only checked on runtime, so FetSerializer.Serialize() MUST have an overload for all subclasses of AbstractConstraint to prevent any unexpected exceptions.
        /// </summary>
        /// <param name="elementName">Name of the containing element</param>
        /// <param name="resources">Timetabling resources</param>
        /// <returns>An XElement.</returns>
        protected XElement SerializeConstraints<T>(string elementName, Dictionary<int, T> resources) where T : AbstractConstraint
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));
            var container = new XElement(elementName);

            // Add elements
            foreach (var element in resources) { container.Add(FetSerializer.Serialize((dynamic)element.Value)); }

            return container;
        }

        protected XElement SerializeDays(Dictionary<int, Day> resources)
        {
            var element = Serialize("Days_List", resources);

            // Add number of days
            element.AddFirst(new XElement("Number_of_Days", resources.Count));
            return element;
        }

        protected XElement SerializeTimeslots(Dictionary<int, Timeslot> resources)
        {
            var element = Serialize("Hours_List", resources);

            // Add number of hours
            element.AddFirst(new XElement("Number_of_Hours", resources.Count));
            return element;
        }


        public static TimetableResourceCollection CreateTestObject()
        {

            var resources = new TimetableResourceCollection
            {


                Days = new Dictionary<int, Day>
                {
                    {1, new Day {Name = "Monday"}}
                },

                Timeslots = new Dictionary<int, Timeslot>
                {
                    {1, new Timeslot {Name = "08:00 - 09:00"}},
                    {2, new Timeslot {Name = "09:00 - 10:00"}},
                    {3, new Timeslot {Name = "10:00 - 11:00"}}
                },

                Rooms = new Dictionary<int, Room>
                {
                    {1, new Room {Id = 1, Name = "Lab"}}
                },

                Subjects = new Dictionary<int, Subject>
                {
                    {2, new Subject {Id = 2, Name = "Computer Science"}}
                },

                Teachers = new Dictionary<int, Teacher>
                {
                    {59, new Teacher {Id = 59, Name = "Sven"}}
                },

                Students = new Dictionary<int, StudentSet>
                {
                    {
                        1, new StudentSet
                        {
                            Id = 1,
                            Name = "2018",
                            Groups = new Dictionary<int, Group>
                            {
                                { 2, new Group
                                    {
                                        Id = 2,
                                        Name = "GroupName",
                                        SubGroups = new Dictionary<int, SubGroup>
                                        {
                                            {3, new SubGroup {Id = 3, Name = "SubgroupName"} }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },

                TimeConstraints = new Dictionary<int, TimeConstraint>
                {
                    { 1, new BasicTimeConstraint() }
                },

                SpaceConstraints = new Dictionary<int, SpaceConstraint>
                {
                    { 1, new BasicSpaceConstraint() }
                }
            };

            resources.Activities = new Dictionary<int, Activity> {
                {
                    1, new Activity()
                    {
                        Id = 1,
                        GroupId = 0,
                        Teacher = resources.GetValue(59, resources.Teachers),
                        Subject = resources.GetValue(2, resources.Subjects),
                        Students = resources.GetValue(1, resources.Students).Groups[2],
                        Duration = 1,
                        Lessons = 10
                    }
                }
            };

            return resources;
        }
    }
}
