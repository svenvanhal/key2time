using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Exceptions
{

    [Serializable()]
    public class AlgorithmException : System.Exception
    {
        public AlgorithmException() : base() { }
        public AlgorithmException(string message) : base(message) { }
        public AlgorithmException(string message, System.Exception inner) : base(message, inner) { }
        protected AlgorithmException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }

}
