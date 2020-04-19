namespace Persons.Abstractions
{
    public class GetPersonQueryHandler : IQueryHandler<GetPersonQuery, Person> 
    {
        public Person Handle(GetPersonQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}