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
        private readonly Guid _personId= Guid.NewGuid();

        [TestMethod]
        public void Create_CorrectNameAndDate_CreateWithoutException()
        {
            var _ = Person.Create(_personId, correctName,_correctDate);
        }

        [TestMethod]
        public void Create_NullName_PersonIsNull()
        {
            var result = Person.Create(_personId, null,_correctDate);

            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void Create_AgeMore120_PersonIsNull()
        {
            const string birthDay = "1799-01-01";
            
            var result = Person.Create(_personId, correctName,Converter.ToDateTime(birthDay));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_AgeLessOrEqual120_PersonNotNull()
        {
            const string birthDay = "1977-07-07";
            
            var result = Person.Create(_personId, correctName,Converter.ToDateTime(birthDay));

            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void ToString_CorrectData_StringNotNull()
        {
            var result = Person.Create(_personId, correctName,_correctDate);

            Assert.IsNotNull(result.ToString());
        }
    }
}