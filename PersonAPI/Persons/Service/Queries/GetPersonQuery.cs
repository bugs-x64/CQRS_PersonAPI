using System;
using Persons.Abstractions;

namespace Persons.Service
{
    public class GetPersonQuery : IQuery
    {
        public Guid Id { get;}
        public GetPersonQuery(Guid id)
        {
            Id = id;
        }
    }
}