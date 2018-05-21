using System;
using Timetabling;
using Timetabling.Algorithms.FET;

namespace Implementation
{
    class Program
    {
        static void Main(string[] args)
        {

            var fet = new FetAlgorithm();
            var input = "./testdata/fet/United-Kingdom/Hopwood/Hopwood.fet";

            var tt = new TimetableGenerator();
            tt.RunAlgorithm(fet, input);

            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
