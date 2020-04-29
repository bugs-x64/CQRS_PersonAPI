using System;
using Nancy;
using Nancy.Responses.Negotiation;
using Persons.Abstractions;
using Persons.Abstractions.Queries;
using Persons.Service.Constants;
using Persons.Service.Dto;
using Persons.Service.Exceptions;
using Persons.Service.Models;

namespace Persons.Modules
{
    /// <summary>
    /// Модуль выполнения запроса.
    /// </summary>
    public sealed class PersonsQueriesModule : NancyModule
    {
        /// <summary>
        /// Базовый путь модуля.
        /// </summary>
        private const string path = RouteConstants.Root + RouteConstants.Version + "/persons";

        /// <summary>
        /// Обработчки запросов на получение <see cref="PersonDto"/>.
        /// </summary>
        private readonly IQueryHandler<GetPersonQuery, PersonDto> _queryHandler;

        public PersonsQueriesModule(IQueryHandler<GetPersonQuery, PersonDto> queryHandler) : base(path)
        {
            _queryHandler = queryHandler;

            Get("/{id}", value => GetPerson(value.id));
        }

        /// <summary>
        /// Выполняет запрос на получение <see cref="PersonDto"/> по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор сущности в репозитории.</param>
        private Negotiator GetPerson(string id)
        {
            Guid guid;
            try
            {
                guid = Guid.Parse(id);
            }
            catch (FormatException)
            {
                return NotFound();
            }

            try
            {
                var getPersonQuery = new GetPersonQuery(guid);
                var result = _queryHandler.Handle(getPersonQuery);

                return Negotiate
                    .WithModel(result);
            }
            catch(EntityNotFoundException<Person>)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Ответ при неправильном идентификаторе запрашиваемой сущности.
        /// </summary>
        private Negotiator NotFound()
        {
            return Negotiate
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithModel("NotFound");//если не добавить такую строку, при открытии в браузере будет падать ошибка 500(актуально только для get запросов).
        }
    }
}