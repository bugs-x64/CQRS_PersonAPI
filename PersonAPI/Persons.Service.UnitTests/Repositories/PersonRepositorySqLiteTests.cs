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
            var name = "Name";
            var birthDay = "1977-07-07";
            var person = Person.Create(Guid.NewGuid(),name,Convert.ToDateTime(birthDay));

            _instance.Insert(person);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullData_ThrowArgumentNullException()
        {
            _instance.Insert(null);
        }

        [TestMethod]
        public void Find_CorrectPersonId_ReturnPersonIdEqualsInitial()
        {
            var personId = Guid.NewGuid();
            var name = "Name";
            var birthDay = "1977-07-07";
            var person = Person.Create(personId, name, Convert.ToDateTime(birthDay));

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