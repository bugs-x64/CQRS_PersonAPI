using System;
using System.Runtime.Serialization;
using Persons.Service.Extensions;

namespace Persons.Service.Exceptions
{
    [Serializable]
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException() : base(Resources.EntityNotFoundException.DefaultFormat(nameof(T)))
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}