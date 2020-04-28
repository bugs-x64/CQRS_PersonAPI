using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions;
using Persons.Service.Exceptions;
using Persons.Service.Models;
using Persons.Service.Repositories;

namespace Persons.UnitTests.Service.Repositories
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private IPersonRepository _instance;

        [TestInitialize]
        public void Initialize()
        {
            _instance = new PersonRepositorySqLite();
        }
        
        [TestMethod]
        public void Insert_WhenCorrectPersonData_RunWithoutException()
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
        public void Insert_WhenNullData_ThrowArgumentNullException()
        {
            _instance.Insert(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Insert_WhenIncorrectPersonId_ThrowFormatException()
        {
            var person = new Person {Id = ""};

            _instance.Insert(person);
        }

        
        [TestMethod]
        public void Find_WhenCorrectPersonId_ReturnPersonIdEqualsInitial()
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
        public void Find_WhenNonexistentPersonId_ThrowEntityNotFoundException()
        {
            var person = Guid.NewGuid();

           _instance.Find(person);
        }
    }
}