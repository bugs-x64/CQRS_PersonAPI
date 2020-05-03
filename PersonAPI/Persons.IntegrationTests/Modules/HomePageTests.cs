using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.IntegrationTests.Fixtures;

namespace Persons.IntegrationTests.Modules
{
    [TestClass]
    public class HomePageTests
    {
        private readonly string _url = GlobalParameters.Host;

        [TestMethod]
        [Timeout(GlobalParameters.TestTimeoutMilliseconds)]
        public async Task Home_WhenGetRequest_ReturnOk()
        {
           var message = await _url
                .WithTimeout(GlobalParameters.RequestTimeoutSeconds)
                .GetAsync();

           Assert.AreEqual(HttpStatusCode.OK,message.StatusCode);
        }
    }
}