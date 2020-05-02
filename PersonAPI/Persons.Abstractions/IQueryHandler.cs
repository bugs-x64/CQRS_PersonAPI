using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Интерфейс обработчика запроса.
    /// </summary>
    /// <typeparam name="TQuery">Запрос.</typeparam>
    /// <typeparam name="TResult">Результат выполнения запроса.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery
    {
        /// <summary>
        /// Выполняет запрос <typeparamref name="TQuery"/> с результатом <typeparamref name="TResult"/>.
        /// </summary>
        /// <param name="query">Выполняемый запрос.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если пришла пустая команда.</exception>
        TResult Handle(TQuery query);
    }
}