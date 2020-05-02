using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions.Queries;

namespace Persons.Abstractions.UnitTests.Queries
{
    [TestClass]
    public class GetPersonQueryTests
    {
        [TestMethod]
        public void Initialization_NewGuid_CreateWithoutException()
        {
            var _ = new GetPersonQuery(Guid.NewGuid());
        }

        [TestMethod]
        public void Initialization_EmptyGuid_CreateWithoutException()
        {
            var _ = new GetPersonQuery(Guid.Empty);
        }


        [TestMethod]
        public void Initialization_EmptyGuid_CreateWithoutException1()
        {
            throw new Exception();
            var _ = new GetPersonQuery(Guid.Empty);
        }
    }
}