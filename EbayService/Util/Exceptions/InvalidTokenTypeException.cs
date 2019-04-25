using System;
using System.Runtime.Serialization;

namespace EbayService.Util
{
    [Serializable]
    internal class InvalidTokenTypeException : Exception
    {
        public InvalidTokenTypeException()
        {
        }

        public InvalidTokenTypeException(string message) : base(message)
        {
        }

        public InvalidTokenTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTokenTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}