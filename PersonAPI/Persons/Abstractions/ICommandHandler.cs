using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Интерфейс обработчика команд.
    /// </summary>
    /// <typeparam name="TCommand">Команда.</typeparam>
    /// <typeparam name="TResult">Результат выполнения команды.</typeparam>
    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand
    {
        /// <summary>
        /// Выполняет команду <typeparamref name="TCommand"/> с результатом <typeparamref name="TResult"/>.
        /// </summary>
        /// <param name="command">выполняемая команда.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если пришла пустая команда.</exception>
        TResult Execute(TCommand command);
    }
}