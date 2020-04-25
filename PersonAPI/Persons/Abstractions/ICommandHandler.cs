using System;

namespace Persons.Abstractions
{
    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand
    {
        /// <exception cref="ArgumentNullException"></exception>
        TResult Execute(TCommand command);
    }
}