using System;
using Autofac;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Routing;
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

            Get("/{id}",_=>CreatePerson());
        }
        
        private Negotiator CreatePerson()
        {
            PersonDto person;
            try
            {
                 person = this.Bind<PersonDto>();
            }
            catch
            {
                return Negotiate.WithStatusCode(HttpStatusCode.BadRequest).WithReasonPhrase("Ошибка десериализации");
            }

            var getPersonQuery = new GetPersonQuery(Guid.Empty);
            _queryHandler.Handle(getPersonQuery);
            
            return Negotiate.WithModel(new PersonDto()).WithHeader("Location","");
        }
    }
}