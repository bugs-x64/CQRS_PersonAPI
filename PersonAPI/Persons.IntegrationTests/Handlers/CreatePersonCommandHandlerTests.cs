using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.IntegrationTests.Fixtures;
using Persons.Service.Exceptions;
using Persons.Service.Models;

namespace Persons.IntegrationTests.Handlers
{
    [TestClass]
    public class CreatePersonCommandHandlerTests
    {
        private ICommandHandler<CreatePersonCommand, Guid> _handler;
        
        [TestInitialize]
        public void Initialize()
        {
            var container = Fixture.RegisterTypes();
            _handler = container.Resolve<ICommandHandler<CreatePersonCommand, Guid>>();
        }
        
        [TestMethod]
        public void Handle_CorrectCommand_ReturnNotEmptyGuid()
        {
            var command = new CreatePersonCommand("john","1977-01-01");

            var guid = _handler.Execute(command);

            Assert.AreNotEqual(Guid.Empty,guid);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Handle_NullQuery_ThrowArgumentNullException()
        {
            _handler.Execute(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(UnprocessableEntityException<Person>))]
        public void Handle_IncorrectCommand_ThrowUnprocessableEntityException()
        {
            var command = new CreatePersonCommand("kk","wrong");

            _handler.Execute(command);
        }
    }
}