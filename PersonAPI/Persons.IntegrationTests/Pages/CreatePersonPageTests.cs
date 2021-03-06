﻿using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.IntegrationTests.Fixtures;
using Persons.Service.Dto;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Persons.IntegrationTests.Pages
{
    [TestClass]
    public class CreatePersonPageTests
    {
        private readonly string _url = GlobalParameters.Host + "/api/v1/persons";
        
        [TestMethod]
        public async Task CreatePerson_CorrectData_ReturnCreated()
        {
            var person = new PersonDto {Name = "person", BirthDay = "1999-09-09"};

            var message = await _url
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .PostJsonAsync(person);

            Assert.AreEqual(HttpStatusCode.Created,message.StatusCode);
        }

        [TestMethod]
        public async Task  CreatePerson_CorrectData_ReturnLocationHeader()
        {
            var person = new PersonDto {Name = "person", BirthDay = "1999-09-09"};

            var message = await _url
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .PostJsonAsync(person);

            Assert.IsTrue(message.Headers.Contains("Location"));
        }


        /// <summary>
        /// По непонятной причине, кейсы на ошибки выполнения запросов не проходили из-за вызова Negotiate.WithReasonPhrase(e.Message) в реализации http метода.
        /// </summary>
        [TestMethod]
        public async Task  CreatePerson_NotJsonData_ReturnBadRequest()
        {
            const string data = "wrongString";

            try
            {
                await _url
                    .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                    .PostStringAsync(data);
            }
            catch (FlurlHttpException e)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest,e.Call.HttpStatus);
            }
        }

        /// <summary>
        /// Наблюдалась проблема при проходжении кейса см. описание <see cref="CreatePerson_NotJsonData_ReturnBadRequest"/>.
        /// </summary>
        [TestMethod]
        public async Task  CreatePerson_NotValidData_ReturnUnprocessableEntity()
        {
            const int unprocessableEntity = 422;
            var person = new PersonDto {Name = "person", BirthDay = "person"};
            
            try
            {await _url
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .PostJsonAsync(person);
            }
            catch (FlurlHttpException e)
            {
                Assert.AreEqual(unprocessableEntity,(int?)e.Call.HttpStatus);
            }
        }
    }
}
