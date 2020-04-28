using System;
using Persons.Abstractions;
using Persons.Service;
using Persons.Service.Dto;
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

        /// <inheritdoc />
        public PersonDto Handle(GetPersonQuery query)
        {
            if (query is null) 
                throw new ArgumentNullException(nameof(query),Resources.NullQueryBody);

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