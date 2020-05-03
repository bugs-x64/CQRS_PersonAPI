using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions.Queries;
using Persons.Handlers;
using Persons.Service.Repositories;

namespace Persons.UnitTests
{
    [TestClass]
    public class GetPersonQueryHandlerTests
    {
        private GetPersonQueryHandler _handler;
        
        [TestInitialize]
        public void Initialize()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization{ConfigureMembers = true});

            _handler = new GetPersonQueryHandler(fixture.Create<IPersonRepository>());
        }
        
        [TestMethod]
        public void Handle_CorrectQuery_ReturnNotNullPersonDto()
        {
            var query = new GetPersonQuery( Guid.NewGuid());

            var dto = _handler.Handle(query);

            Assert.IsNotNull(dto);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Handle_NullQuery_ThrowArgumentNullException()
        {
            _handler.Handle(null);
        }
    }
}
