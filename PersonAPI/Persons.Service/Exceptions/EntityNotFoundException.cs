using System;
using System.Runtime.Serialization;
using Persons.Service.Extensions;

namespace Persons.Service.Exceptions
{
    /// <summary>
    /// Ошибка не найденной сущности.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    [Serializable]
    public class EntityNotFoundException<TEntity> : Exception
    {
        public EntityNotFoundException() : base(Resources.EntityNotFoundException.DefaultFormat(typeof(TEntity)))
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