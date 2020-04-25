using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Service.Models;

namespace Persons.UnitTests.Service.Models
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void Initialization_WhenCorrectNameAndDate_CreateWithoutException()
        {
            var name = "Test";
            var birthDay = "1977-07-07";
            
            var result = new Person {Name = name, BirthDay = birthDay};
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Initialization_WhenIncorrectDate_ThrowFormatException()
        {
            var birthDay = "wrong";

            var result = new Person {BirthDay = birthDay};
        }
        
        [TestMethod]
        public void Initialization_WhenCorrectDate_InitialValueEqualsAssigned()
        {
            var birthDay = "1977-07-07";

            var result = new Person {BirthDay = birthDay};

            Assert.AreEqual(birthDay,result.BirthDay);
        }

        [TestMethod]
        public void Initialization_WhenCorrectName_InitialValueEqualsAssigned()
        {
            var name = "name";

            var result = new Person {Name = name};

            Assert.AreEqual(name,result.Name);
        }
        
        [TestMethod]
        public void Initialization_WhenAgeMore120_AgeIsNull()
        {
            var birthDay = "1799-01-01";

            var result = new Person {BirthDay = birthDay};

            Assert.IsNull(result.Age);
        }

        [TestMethod]
        public void Initialization_WhenAgeLessOrEqual120_AgeNotNull()
        {
            var birthDay = "1977-07-07";

            var result = new Person {BirthDay = birthDay};

            Assert.IsNotNull(result.Age);
        }
    }
}