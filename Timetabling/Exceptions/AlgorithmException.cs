using System;
using Timetabling.Algorithms;

namespace Timetabling.Exceptions
{

    /// <summary>
    /// Exceptions to be thrown from <see cref="Algorithm"/> classes.
    /// Only use when the exception is related to the algorithm logic.
    /// </summary>
    [Serializable()]
    public class AlgorithmException : System.Exception
    {

        /// <summary>
        /// AlgorithmException.
        /// </summary>
        public AlgorithmException() : base() { }

        /// <summary>
        /// AlgorithmException.
        /// </summary>
        /// <param name="message">Error message.</param>
        public AlgorithmException(string message) : base(message) { }

        /// <summary>
        /// AlgorithmException.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="inner">The Exception instance that caused the current exception.</param>
        public AlgorithmException(string message, System.Exception inner) : base(message, inner) { }

        /// <summary>
        /// Serializable AlgorithmException for use by a remote object. 
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected AlgorithmException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
