using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.Handlers;
using Persons.Service.Exceptions;
using Persons.Service.Models;
using Persons.Service.Repositories;

namespace Persons.UnitTests
{
    [TestClass]
    public class CreatePersonCommandHandlerTests
    {
        private CreatePersonCommandHandler _handler;
        
        [TestInitialize]
        public void Initialize()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization{ ConfigureMembers = true });
            _handler = new CreatePersonCommandHandler(fixture.Create<IPersonRepository>());
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