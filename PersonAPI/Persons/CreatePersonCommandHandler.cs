using System;
using Persons.Abstractions;
using Persons.Service.Commands;
using Persons.Service.Exceptions;
using Persons.Service.Models;

namespace Persons
{
    /// <summary>
    /// Обработчик команды на создание <see cref="Person"/> в репозитории.
    /// </summary>
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand,Guid>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <inheritdoc />
        public Guid Execute(CreatePersonCommand command)
        {
            if(command is null)
                throw new ArgumentNullException(nameof(command));

            var newPersonGuid = Guid.NewGuid();
            Person person;
            try
            {
                person = new Person
                {
                    BirthDay = command?.BirthDay,
                    Name = command?.Name,
                    Id = newPersonGuid.ToString()
                };
            }
            catch (Exception e)
            {
                throw new UnprocessableEntityException<Person>(e);
            }

            _personRepository.Insert(person);

            return newPersonGuid;
        }
    }
}