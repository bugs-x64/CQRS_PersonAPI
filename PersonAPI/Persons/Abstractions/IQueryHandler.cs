namespace Persons.Abstractions
{
    /// <summary>
    /// Интерфейс запроса.
    /// </summary>
    /// <typeparam name="TQuery">Класс реализующий интерфейс IQuery.</typeparam>
    /// <typeparam name="TResult">Тип возращаемого результата.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}