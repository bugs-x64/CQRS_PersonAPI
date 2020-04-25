using System;
using Persons.Abstractions;
using Persons.Service.Extensions;

namespace Persons.Service.Commands
{
    /// <summary>
    /// Создать запись о личности.
    /// </summary>
    public class CreatePersonCommand : ICommand
    {
        public CreatePersonCommand(string name, string birthDay)
        {
            if (name.IsNullOrEmpty() || birthDay.IsNullOrEmpty())
                throw new Exception(Resources.CreateCommandException.DefaultFormat(nameof(CreatePersonCommand)));

            Name = name;
            BirthDay = birthDay;
        }

        public string Name { get; }

        public string BirthDay { get; }
    }
}