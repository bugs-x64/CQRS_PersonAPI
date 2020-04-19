using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Обработчик команд для Persons.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class PersonCommandHandler<TCommand, TResult> : ICommandHandler<TCommand,TResult> where TCommand : ICommand
    {
        public TResult Execute(TCommand query)
        {

            throw new NotImplementedException();
        }
    }
}