using Persons.Logging;

namespace Persons.Abstractions
{
    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand
    {
        TResult Execute(TCommand query);
    }
}