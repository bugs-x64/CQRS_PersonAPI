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

        public PersonsCommandsModule(ICommandHandler<CreatePersonCommand,Guid> commandHandler) : base(path)
        {
            _commandHandler = commandHandler;

            Post("/", _ => CreatePerson());
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
                return Negotiate
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithReasonPhrase("Ошибка десериализации");
            }

            var createPersonCommand = new CreatePersonCommand(person.Name, person.BirthDate);

            var result = _commandHandler.Execute(createPersonCommand);

            return Negotiate
                .WithModel(new PersonDto())
                .WithHeader("Location", path + "/" + result);
        }
    }
}