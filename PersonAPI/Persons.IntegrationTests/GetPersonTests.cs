using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Persons.Service.Dto;

namespace Persons.IntegrationTests
{
    [TestClass]
    public class GetPersonTests
    {
        private readonly string _url = GlobalParameters.Host + "/api/v1/persons";
        private string _personPathSegment;

        [TestInitialize]
        [Timeout(GlobalParameters.TestTimeoutMilliseconds)]
        public async Task Initialize()
        {
            var person = new PersonDto
            {
                Name = "GetPersonTests",
                BirthDay = "1964-05-05"
            };

           var result = await _url
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .PostJsonAsync(person);

           _personPathSegment = result.Headers.Location.ToString();
        }

        [TestMethod]
        [Timeout(GlobalParameters.TestTimeoutMilliseconds)]
        public async Task GetPerson_WhenCorrectData_ReturnOk()
        {
            var message = await (GlobalParameters.Host + _personPathSegment)
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .GetAsync();

          Assert.AreEqual(HttpStatusCode.OK,message.StatusCode);
        }
        
        [TestMethod]
        [Timeout(GlobalParameters.TestTimeoutMilliseconds)]
        public async Task  GetPerson_WhenCorrectData_ReturnJsonDto()
        {
            var message = await GlobalParameters.Host
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .AppendPathSegment(_personPathSegment)
                .GetStringAsync();

            var qwe = JsonConvert.DeserializeObject<PersonDto>(message);
        }

        [TestMethod]
        [Timeout(GlobalParameters.TestTimeoutMilliseconds)]
        public async Task  GetPerson_WhenNonexistentId_ReturnNotFound()
        {
            try
            {
                await _url
                    .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                    .AppendPathSegment($"{_personPathSegment}/{Guid.NewGuid()}")
                    .GetAsync();
            }
            catch (FlurlHttpException e)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, e.Call.HttpStatus);
            }
        }

        [TestMethod]
        [Timeout(GlobalParameters.TestTimeoutMilliseconds)]
        public async Task  GetPerson_WhenIncorrectId_ReturnNotFound()
        {
            try
            {
                await _url
                    .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                    .AppendPathSegment($"{_personPathSegment}/wrong")
                    .GetAsync();
            }
            catch (FlurlHttpException e)
            {
                Assert.AreEqual(HttpStatusCode.NotFound,e.Call.HttpStatus);
            }
        }
    }
}
