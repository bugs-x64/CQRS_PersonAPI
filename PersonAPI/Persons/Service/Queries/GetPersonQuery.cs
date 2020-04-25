using System;
using Persons.Abstractions;

namespace Persons.Service.Queries
{
    public class GetPersonQuery : IQuery
    {
        public GetPersonQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}