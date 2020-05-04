using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Persons.Abstractions;
using Persons.Abstractions.Queries;
using Persons.IntegrationTests.Fixtures;
using Persons.Modules;
using Persons.Service.Dto;
using Persons.Service.Models;
using Persons.Service.Repositories;

namespace Persons.IntegrationTests.Modules
{
    [TestClass]
    public class PersonsQueriesModuleTests
    {
        private PersonsQueriesModule _fixtureModule;
        private Guid _newGuid;

        [TestInitialize]
        public void Initialize()
        {
            var testsFixture = TestsFixture.RegisterTypes();
            var repository = testsFixture.Resolve<IPersonRepository>();
            repository.Insert(Person.Create(_newGuid = Guid.NewGuid(), "yrtyrty",DateTime.Today));

            _fixtureModule = new PersonsQueriesModule(testsFixture.Resolve<IQueryHandler<GetPersonQuery, PersonDto>>())
            {
                Context = new NancyContext()
            };
        }
        
        [TestMethod]
        public void GetPerson_CorrectData_ReturnOk()
        {
            var result = _fixtureModule.GetPerson(_newGuid.ToString());

            Assert.AreEqual(HttpStatusCode.OK,result.NegotiationContext.StatusCode);
        }
        
        [TestMethod]
        public void GetPerson_CorrectData_ReturnNotNullBody()
        {
            var result = _fixtureModule.GetPerson(_newGuid.ToString());

            var resultBody = result.NegotiationContext.DefaultModel;

            Assert.IsNotNull(resultBody);
        }

        [TestMethod]
        public void GetPerson_NonexistentId_ReturnNotFound()
        {
            var result = _fixtureModule.GetPerson(Guid.NewGuid().ToString());

            Assert.AreEqual(HttpStatusCode.NotFound,result.NegotiationContext.StatusCode);
        }

        [TestMethod]
        public void GetPerson_IncorrectId_ReturnNotFound()
        {
            var result = _fixtureModule.GetPerson("wrongid");

            Assert.AreEqual(HttpStatusCode.NotFound,result.NegotiationContext.StatusCode);
        }
    }
}
