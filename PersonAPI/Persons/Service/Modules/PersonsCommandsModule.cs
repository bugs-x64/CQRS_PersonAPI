using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Persons.Abstractions;
using Persons.Service.Commands;
using Persons.Service.Constants;
using Persons.Service.Dto;
using Persons.Service.Exceptions;
using Persons.Service.Models;

namespace Persons.Service.Modules
{
    public sealed class PersonsCommandsModule : NancyModule
    {
        private const string path = RouteConstants.Root + RouteConstants.Version + "/persons";
        private readonly ICommandHandler<CreatePersonCommand, Guid> _commandHandler;

        public PersonsCommandsModule(ICommandHandler<CreatePersonCommand, Guid> commandHandler) : base(path)
        {
            _commandHandler = commandHandler;

            Post("/", _ => CreatePerson());
        }

        private object CreatePerson()
        {
            if (!TryGetCommand(out var createPersonCommand, out var negotiator))
                return negotiator;

            try
            {
                var personId = _commandHandler.Execute(createPersonCommand);
                return Negotiate
                    .WithModel("Created")
                    .WithStatusCode(HttpStatusCode.Created)
                    .WithHeader("Location", path + "/" + personId);
            }
            catch (UnprocessableEntityException<Person> e)
            {
                return Negotiate
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                    .WithReasonPhrase(e.Message);
            }
        }

        private bool TryGetCommand(out CreatePersonCommand createPersonCommand, out Negotiator negotiator)
        {
            createPersonCommand = null;
            negotiator = null;
            try
            {
                var person = this.Bind<PersonDto>();
                createPersonCommand = new CreatePersonCommand(person.Name, person.BirthDay);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                //todo разобраться с badrequest, postman почему то ожидает ответа, если пришли некорретные данные
                negotiator = Negotiate
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithReasonPhrase(Resources.UnableToCreateCommandFromReceivedData);

                return false;
            }

            return true;
        }
    }
}