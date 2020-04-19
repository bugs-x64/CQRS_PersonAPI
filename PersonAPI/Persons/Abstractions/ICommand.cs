namespace Persons.Abstractions
{
    /// <summary>
    /// Интерфейс команды.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполнить команду, 
        /// </summary>
        void Execute();
    }
}