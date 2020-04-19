using System;

namespace Persons.Abstractions
{
    public class GetPersonQuery : IQuery<Person>
    {
        public Person Execute()
        {
            throw new NotImplementedException();
        }
    }
}