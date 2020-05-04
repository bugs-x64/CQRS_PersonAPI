using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Logging;
using Persons.Modules;

namespace Persons.IntegrationTests.Modules
{
    [TestClass]
    public class HomeModuleTests
    {
        private HomeModule _module;

        [TestInitialize]
        public void Initialize()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization{ ConfigureMembers = true });
            _module = new HomeModule(fixture.Create<ILog>());
        }

        [TestMethod]
        public void GetHome_CallMethod_NotNull()
        {
            var result = _module.GetHome();

            Assert.IsNotNull(result);
        }
    }
}