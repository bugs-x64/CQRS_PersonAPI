using System;
using Nancy;
using Nancy.Responses.Negotiation;
using Persons.Abstractions;
using Persons.Service.Constants;
using Persons.Service.Dto;
using Persons.Service.Queries;

namespace Persons
{
    public sealed class PersonsQueriesModule : NancyModule
    {
        private const string path = RouteConstants.Root + RouteConstants.Version + "/persons";
        private readonly IQueryHandler<GetPersonQuery, PersonDto> _queryHandler;

        public PersonsQueriesModule(IQueryHandler<GetPersonQuery, PersonDto> queryHandler) : base(path)
        {
            _queryHandler = queryHandler;

            Get("/{id}", value => GetPerson(value.id));
        }

        private Negotiator GetPerson(string id)
        {
            Guid guid;
            try
            {
                guid = Guid.Parse(id);
            }
            catch (FormatException)
            {
                return Negotiate
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithModel("BadRequest");
            }

            var getPersonQuery = new GetPersonQuery(guid);
            var result = _queryHandler.Handle(getPersonQuery);

            return Negotiate
                .WithModel(result);
        }
    }
}