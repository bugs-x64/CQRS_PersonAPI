using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Newtonsoft.Json;
using Persons.Abstractions;
using Persons.Logging;
using Persons.Service;
using Persons.Service.Commands;
using Persons.Service.Constants;
using Persons.Service.Dto;
using Persons.Service.Exceptions;
using Persons.Service.Extensions;
using Persons.Service.Models;

namespace Persons
{
    public sealed class PersonsCommandsModule : NancyModule
    {
        private const string path = RouteConstants.Root + RouteConstants.Version + "/persons";
        private readonly ICommandHandler<CreatePersonCommand, Guid> _commandHandler;
        private readonly ILog _log;

        public PersonsCommandsModule(ICommandHandler<CreatePersonCommand, Guid> commandHandler, ILog log) : base(path)
        {
            _commandHandler = commandHandler;
            _log = log;

            Post("/", _ => CreatePerson());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private Negotiator CreatePerson()
        {
            try
            {
                var createPersonCommand = GetCommand();
                var personId = _commandHandler.Execute(createPersonCommand);
                return Negotiate
                    .WithModel("Created")
                    .WithStatusCode(HttpStatusCode.Created)
                    .WithHeader("Location", path + "/" + personId);
            }
            catch (UnprocessableEntityException<Person> e)
            {
                return UnprocessableEntity(e);
            }
            catch (JsonReaderException)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        private Negotiator UnprocessableEntity(UnprocessableEntityException<Person> e)
        {
            _log.Log(LogLevel.Debug, () => e.Message);
            return Negotiate
                .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                .WithReasonPhrase(e.Message);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private CreatePersonCommand GetCommand()
        {
                var readToString = Request?.Body?.ReadToString();
                _log.Log(LogLevel.Debug, () => $"Поступила команда:{Environment.NewLine}{readToString}");

                var person = JsonConvert.DeserializeObject<PersonDto>(readToString);

                return new CreatePersonCommand(person.Name, person.BirthDay);
        }
        
        /// <summary>
        /// Ответ при неправильных данных от пользователя.
        /// </summary>
        private Negotiator BadRequest()
        {
            _log.Log(LogLevel.Debug, () => Resources.UnableToCreateCommandFromReceivedData);
            return Negotiate
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithReasonPhrase(Resources.UnableToCreateCommandFromReceivedData);
        }

        /// <summary>
        /// Ответ при внутренней ошибке сервера.
        /// </summary>
        private Negotiator InternalError(Exception e)
        {
            _log.Log(LogLevel.Debug, () => e.Message);
            return Negotiate
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithReasonPhrase(e.Message);
        }
    }
}