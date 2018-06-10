using System;
using System.Threading.Tasks;
using Implementation.Key2Soft;
using Timetabling;
using Timetabling.Algorithms;
using Timetabling.Algorithms.FET;
using Timetabling.DB;
using Timetabling.Resources;

namespace Implementation
{
    class Program
    {
        static void Main(string[] args)
        {

            // Example usage:
            //   1 - Instantiate and configure algorithm to use
            //   2 - Specify input data for algorithm
            //   3 - Create a TimetableGenerator
            //   4 - Let the TimetableGenerator generate a Task<Timetable>
            //   5 - Do something with the Timetable output object when the Task finishes

            //var inputGenerator = new Timetabling.Algorithms.FET.FetInputGenerator();
            //inputGenerator.BuildXml(resources);
            //return;
            TimetablingStrategy algorithm = new FetAlgorithm();

            var program = new Program();
            //var resources = program.GetResources();
            var resources = Timetabling.Algorithms.FET.FetInputGenerator.CreateTestObject();

            var generator = new TimetableGenerator();
            var task = generator.RunAlgorithm(algorithm, resources);


            task.ContinueWith(OnSuccess, TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(OnError, TaskContinuationOptions.OnlyOnFaulted);
            
            Console.Read();
        }

        public TimetableResourceCollection GetResources()
        {
            var model = new DataModel();

            var resources = new TimetableResourceCollection
            {
                Days = DboResourceFactory.GetDays(),
                Timeslots = DboResourceFactory.GetTimeslots(),
                Teachers = DboResourceFactory.GetTeachers(model),
                Subjects = DboResourceFactory.GetSubjects(model),
                Rooms = DboResourceFactory.GetRooms(model),
                Students = DboResourceFactory.GetYears(model),
            };

            resources.Activities = DboResourceFactory.GetActivities(model, resources);
            resources.TimeConstraints = DboResourceFactory.GetTimeConstraints(model, resources);
            resources.SpaceConstraints = DboResourceFactory.GetSpaceConstraints(model, resources);

            return resources;
        }

        public static void OnSuccess(Task<Timetable> t)
        {
            Console.WriteLine("The timetable has been generated sucessfully.");
            Console.WriteLine(t);

            Console.Read();
        }

        public static void OnError(Task<Timetable> t)
        {
            

            var exs = t.Exception.InnerExceptions;
            
            foreach (var e in exs)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("The timetable could not be generated.");

            Console.Read();
        }
    }
}
