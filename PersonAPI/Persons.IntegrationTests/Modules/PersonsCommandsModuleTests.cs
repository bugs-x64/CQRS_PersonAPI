using System;
using System.IO;
using System.Text;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Newtonsoft.Json;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.IntegrationTests.Fixtures;
using Persons.Logging;
using Persons.Modules;

namespace Persons.IntegrationTests.Modules
{
    [TestClass]
    public class PersonsCommandsModuleTests
    {
        private PersonsCommandsModule _fixtureModule;

        [TestInitialize]
        public void Initialize()
        {
            var testsFixture = TestsFixture.RegisterTypes();

            _fixtureModule = new PersonsCommandsModule(testsFixture.Resolve<ICommandHandler<CreatePersonCommand, Guid>>(),testsFixture.Resolve<ILog>());
        }
        
        [TestMethod]
        public void CreatePerson_CorrectData_NotNull()
        {
            var jsonString = JsonConvert.SerializeObject(new
            {
                Name = "Name",
                BirthDay = "1990-09-09"
            });

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var moduleRequest = new Request("POST", new Url("http://someURL"), memoryStream);
                _fixtureModule.Context = new NancyContext();
                _fixtureModule.Request = moduleRequest;

                var result = _fixtureModule.CreatePerson();

                Assert.IsNotNull(result);
            }
        }
        
        [TestMethod]
        public void CreatePerson_CorrectData_ReturnCreated()
        {
            var jsonString = JsonConvert.SerializeObject(new
            {
                Name = "Name",
                BirthDay = "1990-09-09"
            });

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var moduleRequest = new Request("POST", new Url("http://someURL"), memoryStream);
                _fixtureModule.Context = new NancyContext();
                _fixtureModule.Request = moduleRequest;

                var result = _fixtureModule.CreatePerson();
                
                Assert.AreEqual(HttpStatusCode.Created,result.NegotiationContext.StatusCode);
            }
        }

        [TestMethod]
        public void  CreatePerson_CorrectData_ReturnLocationHeader()
        {
            var jsonString = JsonConvert.SerializeObject(new
            {
                Name = "Name",
                BirthDay = "1990-09-09"
            });

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var moduleRequest = new Request("POST", new Url("http://someURL"), memoryStream);
                _fixtureModule.Context = new NancyContext();
                _fixtureModule.Request = moduleRequest;

                var result = _fixtureModule.CreatePerson();
                
                Assert.IsTrue(result.NegotiationContext.Headers.ContainsKey("Location"));
            }
        }
        
        [TestMethod]
        public void CreatePerson_NotJsonData_ReturnBadRequest()
        {
            const string data = "wrongString";

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                var moduleRequest = new Request("POST", new Url("http://someURL"), memoryStream);
                _fixtureModule.Context = new NancyContext();
                _fixtureModule.Request = moduleRequest;

                var result = _fixtureModule.CreatePerson();
                Assert.AreEqual(HttpStatusCode.BadRequest,result.NegotiationContext.StatusCode);
            }
        }
        

        [DataTestMethod]
        [DataRow("Name", "1800-09-09")]
        public void CreatePerson_NotValidData_ReturnUnprocessableEntity(string name, string birthday)
        {
            const int unprocessableEntity = 422;
            
            var jsonString = JsonConvert.SerializeObject(new
            {
                Name = name,
                BirthDay = birthday
            });
            
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var moduleRequest = new Request("POST", new Url("http://someURL"), memoryStream);
                _fixtureModule.Context = new NancyContext();
                _fixtureModule.Request = moduleRequest;

                var result = _fixtureModule.CreatePerson();
                Assert.AreEqual(unprocessableEntity,(int?)result.NegotiationContext.StatusCode);
            }
        }
    }
}
