using System;
using Persons.Abstractions;
using Persons.Service;

namespace Persons
{
    public class GetPersonQueryHandler : IQueryHandler<GetPersonQuery, PersonDto> 
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public PersonDto Handle(GetPersonQuery query)
        {
            if (query is null) 
                throw new ArgumentException(Resources.NullQueryBody,nameof(query));

            var result = _personRepository.Find(query.Id);

            return new PersonDto
            {
                Id = result.Id,
                Name = result.Name,
                Age = result.Age,
                BirthDay = result.BirthDay
            };
        }
    }
}