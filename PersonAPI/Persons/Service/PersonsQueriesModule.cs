using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Persons.Abstractions;
namespace Persons.Service
{
    public sealed class PersonsQueriesModule:NancyModule
    {
        private readonly IQueryHandler<GetPersonQuery, PersonDto> _queryHandler;
        private const string path = RouteConstants.Root+RouteConstants.Version+"/persons";

        public PersonsQueriesModule(IQueryHandler<GetPersonQuery,PersonDto> queryHandler):base(path)
        {
            _queryHandler = queryHandler;

            Get("/{id}",value=>GetPerson(value.id));
        }
        
        private Negotiator GetPerson(Guid id)
        {
            //todo проработать получение person 
            var getPersonQuery = new GetPersonQuery(id);
           var result =  _queryHandler.Handle(getPersonQuery);
            
            return Negotiate
                .WithModel(result);
        }
    }
}