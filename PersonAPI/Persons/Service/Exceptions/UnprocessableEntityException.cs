﻿using System;
using System.Runtime.Serialization;
using Persons.Service.Extensions;

namespace Persons.Service.Exceptions
{
    /// <summary>
    /// Ошибка валидации сущности.
    /// </summary>
    [Serializable]
    public class UnprocessableEntityException<T> : Exception
    {
        /// <summary>
        /// Генерирует исключение валидации сущности <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Сущность, которую не удалось валидировать.</typeparam>
        public UnprocessableEntityException()
            : base(Resources.UnprocessableEntityException.DefaultFormat(typeof(T)))
        {
        }

        /// <summary>
        /// Генерирует исключение валидации сущности <typeparamref name="T"/> с вложенной ошибкой <paramref name="innerException"/>.
        /// </summary>
        /// <typeparam name="T">Сущность, которую не удалось валидировать.</typeparam>
        public UnprocessableEntityException(Exception innerException)
            : base(Resources.UnprocessableEntityException.DefaultFormat(typeof(T)), innerException)
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