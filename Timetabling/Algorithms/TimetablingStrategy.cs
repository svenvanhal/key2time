using System.Threading;
using System.Threading.Tasks;
using Timetabling.DB;
using Timetabling.Objects;

namespace Timetabling.Algorithms
{

    /// <summary>
    /// Timetabling strategy. Subclasses generate a Timetable task based on a timetabling algorithm.
    /// </summary>
    public abstract class TimetablingStrategy
    {

        /// <summary>
        /// Generates the timetabling task. Executes an algorithm on a data set and returns results. Timetable generation
        /// can be cancelled using the provided CancellationToken.
        /// </summary>
        /// <param name="identifier">Unique identifier for this algorithm run.</param>
        /// <param name="input">Input to run the algorithm on.</param>
        /// <param name="t">Cancellation token.</param>
        /// <returns>A Task-object which ultimately yields a Timetable.</returns>
        protected internal abstract Task<Timetable> GenerateTask(string identifier, DataModel input, CancellationToken t);

    }
}
