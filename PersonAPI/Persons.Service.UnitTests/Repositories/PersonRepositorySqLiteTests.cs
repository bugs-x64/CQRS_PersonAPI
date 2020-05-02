using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Service.Exceptions;
using Persons.Service.Models;
using Persons.Service.Repositories;

namespace Persons.Service.UnitTests.Repositories
{
    [TestClass]
    public class PersonRepositorySqLiteTests
    {
        private IPersonRepository _instance;

        [TestInitialize]
        public void Initialize()
        {
            _instance = new PersonRepositorySqLite();
        }
        
        [TestMethod]
        public void Insert_CorrectPersonData_RunWithoutException()
        {
            var person = new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                BirthDay = "1977-07-07"
            };

            _instance.Insert(person);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullData_ThrowArgumentNullException()
        {
            _instance.Insert(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Insert_EmptyPersonId_ThrowFormatException()
        {
            var person = new Person {Id = string.Empty};

            _instance.Insert(person);
        }

        
        [TestMethod]
        public void Find_CorrectPersonId_ReturnPersonIdEqualsInitial()
        {
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId.ToString(),
                Name = "Name",
                BirthDay = "1977-07-07"
            };
            _instance.Insert(person);

           var result = _instance.Find(personId);

           Assert.AreEqual(personId.ToString(),result.Id);
        }

        
        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException<Person>))]
        public void Find_NonexistentPersonId_ThrowEntityNotFoundException()
        {
            var person = Guid.NewGuid();

           _instance.Find(person);
        }
    }
}