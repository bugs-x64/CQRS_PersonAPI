using System;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.Service;
using Persons.Service.Exceptions;
using Persons.Service.Models;
using Persons.Service.Repositories;

namespace Persons.Handlers
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
                person = Person.Create(newPersonGuid, command.Name, Converter.ToDateTime(command.BirthDay));

                if(person is null)
                    throw new Exception();
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