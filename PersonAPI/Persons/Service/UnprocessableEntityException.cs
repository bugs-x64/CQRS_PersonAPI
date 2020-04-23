using System;
using System.Runtime.Serialization;
using Nancy;

namespace Persons.Service
{
    /// <summary>
    /// Ошибка валидации сущности.
    /// </summary>
    [Serializable]
    public class UnprocessableEntityException:Exception
    {
        public UnprocessableEntityException()
        {
        }

        public UnprocessableEntityException(string message) : base(message)
        {
        }

        public UnprocessableEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnprocessableEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}