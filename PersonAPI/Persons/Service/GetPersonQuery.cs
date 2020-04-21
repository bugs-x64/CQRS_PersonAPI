using System;
using Persons.Abstractions;

namespace Persons.Service
{
    public class GetPersonQuery : IQuery<PersonDto>
    {
        public Guid Id { get;}
        public GetPersonQuery(Guid id)
        {
            Id = id;
        }
    }
}