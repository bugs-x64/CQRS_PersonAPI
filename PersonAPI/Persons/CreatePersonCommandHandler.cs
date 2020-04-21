using System;
using Persons.Abstractions;
using Persons.Service;

namespace Persons
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand,Guid>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Guid Execute(CreatePersonCommand query)
        {
            var newPersonGuid = Guid.NewGuid();

            var person = new Person
            {
                BirthDate = query.BirthDate,
                Name = query.Name,
                Id = newPersonGuid.ToString()
            };
            _personRepository.Insert(person);

            return newPersonGuid;
        }
    }
}