using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions;
using Persons.Service.Dto;
using Persons.Service.Models;
using Persons.Service.Queries;

namespace Persons.UnitTests
{
    [TestClass]
    public class GetPersonQueryHandlerTests
    {
        private IQueryHandler<GetPersonQuery, PersonDto> _handler;
        private Guid _personId;
        
        [TestInitialize]
        public void Initialize()
        {
            var container = Fixture.RegisterTypes();
            var repository = container.Resolve<IPersonRepository>();
            _handler = container.Resolve<IQueryHandler<GetPersonQuery, PersonDto>>();
            
            _personId = Guid.NewGuid();
            repository.Insert(new Person { Id = _personId.ToString() });
        }
        
        [TestMethod]
        public void Handle_WhenCorrectQuery_ReturnNotNullPersonDto()
        {
            var query = new GetPersonQuery(_personId);

            var dto = _handler.Handle(query);

            Assert.IsNotNull(dto);
        }

        [TestMethod]
        public void Handle_WhenCorrectQuery_ReturnPersonIdEqualsRequested()
        {
            var query = new GetPersonQuery(_personId);

            var dto = _handler.Handle(query);

            Assert.AreEqual(_personId.ToString(),dto.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Handle_WhenNullQuery_ThrowArgumentNullException()
        {
            _handler.Handle(null);
        }
    }
}
