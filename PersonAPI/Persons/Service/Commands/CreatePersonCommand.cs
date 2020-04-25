using System;
using System.Linq;
using Persons.Abstractions;

namespace Persons.Service
{
    /// <summary>
    /// Создать запись о личности.
    /// </summary>
    public class CreatePersonCommand : ICommand
    {
        public string Name { get; }

        public string BirthDay { get; }

        public CreatePersonCommand(string name, string birthDay)
        {
            if(name.IsNullOrEmpty() || birthDay.IsNullOrEmpty())
                throw new Exception(Resources.CreateCommandException.DefaultFormat(nameof(CreatePersonCommand)));

            Name = name;
            BirthDay = birthDay;
        }
    }
}