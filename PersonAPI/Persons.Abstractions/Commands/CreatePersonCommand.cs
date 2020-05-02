using System;

namespace Persons.Abstractions.Commands
{
    /// <summary>
    /// Создать запись о личности.
    /// </summary>
    public class CreatePersonCommand : ICommand
    {
        public CreatePersonCommand(string name, string birthDay)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name),Resources.CreateCommandException);

            if(string.IsNullOrEmpty(birthDay))
                throw new ArgumentNullException(nameof(birthDay),Resources.CreateCommandException);

            Name = name;
            BirthDay = birthDay;
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public string BirthDay { get; }
    }
}