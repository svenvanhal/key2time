using System;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;
using Timetabling;
using Timetabling.Algorithms.FET;
using Timetabling.DB;

namespace Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var sw = new Stopwatch();
            sw.Start();

            using (var dm = new DataModel())
            {
                dm.Database.Exists();
            }

            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Stop();
            
            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
