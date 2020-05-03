using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Service.Models;

namespace Persons.Service.UnitTests.Models
{
    [TestClass]
    public class PersonTests
    {
        private const string correctName = "Name";
        private readonly DateTime _correctDate = DateTime.Parse("1977-07-07");
        
        [TestMethod]
        public void Create_CorrectNameAndDate_CreateWithoutException()
        {
            var _ = Person.Create(Guid.NewGuid(), correctName,_correctDate);
        }

        [TestMethod]
        public void Create_NullName_PersonIsNull()
        {
            var result = Person.Create(Guid.NewGuid(), null,_correctDate);

            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void Create_AgeMore120_PersonIsNull()
        {
            const string birthDay = "1799-01-01";
            
            var result = Person.Create(Guid.NewGuid(), correctName,Converter.ToDateTime(birthDay));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_AgeLessOrEqual120_PersonNotNull()
        {
            const string birthDay = "1977-07-07";
            
            var result = Person.Create(Guid.NewGuid(), correctName,Converter.ToDateTime(birthDay));

            Assert.IsNotNull(result);
        }
    }
}