using System.Threading;
using System.Threading.Tasks;
using Timetabling.Resources;

namespace Timetabling.Algorithms
{

    /// <summary>
    /// Task-based timetable generation.
    /// </summary>
    public abstract class Algorithm
    {

        /// <summary>
        /// Initialize and execute algorithm, return results.
        /// </summary>
        /// <param name="identifier">Unique identifier for this algorithm run.</param>
        /// <param name="input">Input to run the algorithm on.</param>
        /// <param name="t">Cancellation token.</param>
        /// <returns>A Task-object yielding a Timetable.</returns>
        protected internal abstract Task<Timetable> GenerateTask(string identifier, string input, CancellationToken t);

    }
}
