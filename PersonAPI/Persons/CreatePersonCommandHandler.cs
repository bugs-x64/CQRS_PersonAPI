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
            Person person;
            try
            {
                person = new Person
                {
                    BirthDay = query?.BirthDay,
                    Name = query?.Name,
                    Id = newPersonGuid.ToString()
                };
            }
            catch (Exception e)
            {
                throw new UnprocessableEntityException($"Ошибка валидации {typeof(Person)}",e);
            }

            _personRepository.Insert(person);

            return newPersonGuid;
        }
    }
}