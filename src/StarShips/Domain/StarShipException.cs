using System;
using System.Runtime.Serialization;

namespace Domain
{
    /// <summary>
    /// Specialized class exception for better treatments
    /// </summary>
    public class StarShipException : Exception
    {
        public StarShipException()
        {

        }

        public StarShipException(string message) : base(message)
        {

        }

        public StarShipException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected StarShipException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
