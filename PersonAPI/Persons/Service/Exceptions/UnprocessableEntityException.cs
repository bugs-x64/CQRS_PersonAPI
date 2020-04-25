using System;
using System.Runtime.Serialization;

namespace Persons.Service.Exceptions
{
    /// <summary>
    /// Ошибка валидации сущности.
    /// </summary>
    [Serializable]
    public class UnprocessableEntityException<T>:Exception
    {
        /// <summary>
        /// Генерирует исключение валидации сущности <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Сущность, которую не удалось валидировать.</typeparam>
        public UnprocessableEntityException()
            :base(message: Resources.UnprocessableEntityException.DefaultFormat(nameof(T)))
        {
        }

        /// <summary>
        /// Генерирует исключение валидации сущности <typeparamref name="T"/> с вложенной ошибкой <paramref name="innerException"/>.
        /// </summary>
        /// <typeparam name="T">Сущность, которую не удалось валидировать.</typeparam>
        public UnprocessableEntityException(Exception innerException)
            :base(Resources.UnprocessableEntityException.DefaultFormat(nameof(T)), innerException)
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