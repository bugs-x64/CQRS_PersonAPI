using System;
using System.Globalization;

namespace Persons.Service.Exceptions
{
    [Serializable]
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException():base(message: Resources.EntityNotFoundException.DefaultFormat(nameof(T)))
        {
        }

        public EntityNotFoundException(string message):base(message)
        {
        }

        public EntityNotFoundException(string message,Exception innerException):base(message,innerException)
        {
        }

        protected EntityNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext):base(serializationInfo,streamingContext)
        {
        }
    }
}