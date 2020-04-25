using System;
using Persons.Abstractions;
using Persons.Service;
using Persons.Service.Dto;
using Persons.Service.Exceptions;
using Persons.Service.Models;
using Persons.Service.Queries;

namespace Persons
{
    /// <summary>
    /// Обработчик запроса на получение <see cref="PersonDto"/> из репозитория.
    /// </summary>
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

            if (result?.Id is null)
                throw new EntityNotFoundException<Person>();

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