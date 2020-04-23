using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Persons.Abstractions;

namespace Persons.Service
{
    public sealed class PersonsCommandsModule : NancyModule
    {
        private readonly ICommandHandler<CreatePersonCommand, Guid> _commandHandler;
        private const string path = RouteConstants.Root + RouteConstants.Version + "/persons";

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
            catch (UnprocessableEntityException e)
            {
                return Negotiate
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                    .WithReasonPhrase("Cозданная сущность невалидна");
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
            catch
            {
                //todo разобраться с badrequest, postman почему то ожидает ответа, если пришли некорретные данные
                negotiator = Negotiate
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithReasonPhrase("Невозможно создать команду из полученных данных");

                return false;
            }

            return true;
        }
    }
}