using Persons.Abstractions;

namespace Persons.Service
{
    /// <summary>
    /// Создать запись о личности.
    /// </summary>
    public class CreatePersonCommand : ICommand
    {
        public string Name { get; }

        public string BirthDate { get; }

        public CreatePersonCommand(string name, string birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }
    }
}