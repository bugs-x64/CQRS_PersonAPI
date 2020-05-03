using System;

namespace Persons.Abstractions.Queries
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