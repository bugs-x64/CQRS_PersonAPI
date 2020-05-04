using System;
using System.Runtime.Serialization;
using Persons.Service.Extensions;

namespace Persons.Service.Exceptions
{
    /// <summary>
    /// Ошибка валидации сущности.
    /// </summary>
    /// <typeparam name="TEntity">Сущность, которую не удалось валидировать.</typeparam>
    [Serializable]
    public class UnprocessableEntityException<TEntity> : Exception
    {
        /// <summary>
        /// Генерирует исключение валидации сущности <typeparamref name="TEntity"/> с вложенной ошибкой <paramref name="innerException"/>.
        /// </summary>
        public UnprocessableEntityException(Exception innerException)
            : base(Resources.UnprocessableEntityException.DefaultFormat(typeof(TEntity)), innerException)
        {
        }
    }
}